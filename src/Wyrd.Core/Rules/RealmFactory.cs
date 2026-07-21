using Wyrd.Core.Domain;

namespace Wyrd.Core.Rules
{

public static class RealmFactory
{
    public static Realm CreateStarterRealm(RaceType race)
    {
        var realm = new Realm("El Pacto del Cuervo", race, 90);
        var city = new City("Skallgard", "confines-del-norte", true);

        foreach (var definition in GameCatalog.Buildings.Values)
        {
            var startsBuilt = definition.Type == BuildingType.SilverMine
                || definition.Type == BuildingType.Well
                || definition.Type == BuildingType.Fields
                || definition.Type == BuildingType.Pastures
                || definition.Type == BuildingType.Woodcutters
                || definition.Type == BuildingType.IronMine
                || definition.Type == BuildingType.Quarry
                || definition.Type == BuildingType.Market;
            city.AddBuilding(new CityBuilding(definition.Type, startsBuilt ? 1 : 0, startsBuilt ? 50m : 0m));
        }

        realm.AddCity(city);
        realm.Resources.Add(ResourceType.Silver, 200m);
        realm.Resources.Add(ResourceType.Wood, 300m);
        realm.Resources.Add(ResourceType.Stone, 250m);

        var value = new RealmValueCalculator().Calculate(realm);
        realm.RecordValue(0, value);
        return realm;
    }
}
}
