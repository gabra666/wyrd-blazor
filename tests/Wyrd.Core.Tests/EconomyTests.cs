using Wyrd.Core.Domain;
using Wyrd.Core.Rules;
using Xunit;

namespace Wyrd.Core.Tests;

public sealed class EconomyTests
{
    [Fact]
    public void Catalog_contains_the_decided_resources_races_and_buildings()
    {
        Assert.Equal(25, GameCatalog.Resources.Count);
        Assert.Equal(7, GameCatalog.Races.Count);
        Assert.Equal(25, GameCatalog.Buildings.Count);
        Assert.Equal("Hrímþursar", GameCatalog.Races[RaceType.Hrimthursar].Name);
    }

    [Fact]
    public void Level_ten_uses_twenty_five_percent_compound_growth()
    {
        Assert.Equal(7.450580596923828125m, EconomyMath.LevelFactor(10));
    }

    [Fact]
    public void First_day_produces_primaries_and_market_imports()
    {
        var realm = RealmFactory.CreateStarterRealm(RaceType.Midgardsmenn);

        new GameDayService().Advance(realm);

        Assert.Equal(215m, realm.Resources[ResourceType.Silver]);
        Assert.Equal(210m, realm.Resources[ResourceType.Water]);
        Assert.Equal(0.10m, realm.Resources[ResourceType.Whetstone]);
        Assert.Equal(0.05m, realm.Resources[ResourceType.Runestone]);
    }

    [Fact]
    public void Hrimthursar_bonus_stacks_with_the_spring()
    {
        var realm = RealmFactory.CreateStarterRealm(RaceType.Hrimthursar);

        new GameDayService().Advance(realm);

        Assert.Equal(230m, realm.Resources[ResourceType.Water]);
    }

    [Fact]
    public void An_upgrade_reduces_immediate_realm_value_by_twenty_five_percent()
    {
        var realm = RealmFactory.CreateStarterRealm(RaceType.Midgardsmenn);
        var calculator = new RealmValueCalculator();
        var before = calculator.Calculate(realm);

        var upgraded = new BuildingUpgradeService().TryUpgrade(realm, realm.Cities[0], BuildingType.Mill);

        Assert.True(upgraded);
        Assert.Equal(before - 12.5m, calculator.Calculate(realm));
        Assert.Equal(50m, realm.Cities[0].GetBuilding(BuildingType.Mill).InvestedValue);
    }

    [Fact]
    public void A_processor_cannot_exceed_its_producer()
    {
        var realm = RealmFactory.CreateStarterRealm(RaceType.Midgardsmenn);
        var service = new BuildingUpgradeService();
        Assert.True(service.TryUpgrade(realm, realm.Cities[0], BuildingType.Mill));

        var quote = service.GetQuote(realm, realm.Cities[0], BuildingType.Mill);

        Assert.False(quote.CanUpgrade);
        Assert.Contains("Campos", quote.BlockedReason);
    }

    [Fact]
    public void Primary_resources_can_be_processed_during_the_same_day()
    {
        var realm = RealmFactory.CreateStarterRealm(RaceType.Midgardsmenn);
        Assert.True(new BuildingUpgradeService().TryUpgrade(realm, realm.Cities[0], BuildingType.Mill));

        new GameDayService().Advance(realm);

        Assert.Equal(55m, realm.Resources[ResourceType.Food]);
        Assert.Equal(50m, realm.Resources[ResourceType.Grain]);
    }

    [Fact]
    public void Scarce_inputs_are_shared_proportionally()
    {
        var realm = CreateEmptyRealm(RaceType.Midgardsmenn, new Dictionary<BuildingType, int>
        {
            [BuildingType.Sawmill] = 1,
            [BuildingType.CharcoalKiln] = 1
        });
        realm.Resources.Add(ResourceType.Wood, 10m);

        var report = new GameDayService().Advance(realm);

        Assert.Equal(5m, realm.Resources[ResourceType.Planks]);
        Assert.Equal(2m, realm.Resources[ResourceType.Charcoal]);
        Assert.Equal(0m, realm.Resources[ResourceType.Wood]);
        Assert.Equal(2, report.Shortages.Count);
    }

    [Fact]
    public void Dokkalfar_bonus_applies_to_tools()
    {
        var realm = CreateEmptyRealm(RaceType.Dokkalfar, new Dictionary<BuildingType, int> { [BuildingType.Smithy] = 1 });
        realm.Resources.Add(ResourceType.Iron, 3m);
        realm.Resources.Add(ResourceType.Wood, 4m);
        realm.Resources.Add(ResourceType.Charcoal, 1m);

        new GameDayService().Advance(realm);

        Assert.Equal(2.2m, realm.Resources[ResourceType.Tools]);
    }

    [Fact]
    public void Transformed_resource_values_add_ten_percent()
    {
        var planksInput = 12m * GameCatalog.Resources[ResourceType.Wood].UnitValue;
        var planksOutput = 10m * GameCatalog.Resources[ResourceType.Planks].UnitValue;
        var beerInput = 30m * GameCatalog.Resources[ResourceType.Grain].UnitValue + 40m * GameCatalog.Resources[ResourceType.Water].UnitValue;
        var beerOutput = 10m * GameCatalog.Resources[ResourceType.Beer].UnitValue;

        Assert.Equal(planksInput * 1.10m, planksOutput);
        Assert.Equal(beerInput * 1.10m, beerOutput);
    }

    [Fact]
    public void Campaign_finishes_after_resolving_day_ninety()
    {
        var realm = RealmFactory.CreateStarterRealm(RaceType.Midgardsmenn);
        var service = new GameDayService();
        for (var day = 1; day <= 90; day++) service.Advance(realm);

        Assert.True(realm.IsComplete);
        Assert.Equal(90, realm.LastReport!.Day);
        Assert.Equal(91, realm.ValueHistory.Count);
        Assert.Throws<InvalidOperationException>(() => service.Advance(realm));
    }

    private static Realm CreateEmptyRealm(RaceType race, IReadOnlyDictionary<BuildingType, int> levels)
    {
        var realm = new Realm("Test", race);
        var city = new City("Test City", "test");
        foreach (var definition in GameCatalog.Buildings.Values)
        {
            levels.TryGetValue(definition.Type, out var level);
            city.AddBuilding(new CityBuilding(definition.Type, level));
        }
        realm.AddCity(city);
        return realm;
    }
}
