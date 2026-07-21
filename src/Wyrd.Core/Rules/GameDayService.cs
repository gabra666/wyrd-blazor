using System;
using System.Collections.Generic;
using System.Linq;
using Wyrd.Core.Domain;

namespace Wyrd.Core.Rules
{

public sealed class GameDayService
{
    private readonly RealmValueCalculator _valueCalculator = new RealmValueCalculator();

    public DayReport Advance(Realm realm)
    {
        if (realm.IsComplete) throw new InvalidOperationException("The campaign has already ended.");

        var report = new DayReport(realm.CurrentDay, _valueCalculator.Calculate(realm));
        for (var phase = 1; phase <= 3; phase++) ProcessPhase(realm, phase, report);

        report.ValueAfter = _valueCalculator.Calculate(realm);
        realm.LastReport = report;
        realm.RecordValue(realm.CurrentDay, report.ValueAfter);
        realm.CompleteDay();
        return report;
    }

    private static void ProcessPhase(Realm realm, int phase, DayReport report)
    {
        var jobs = new List<Job>();
        foreach (var city in realm.Cities)
        foreach (var building in city.Buildings.Values)
        {
            if (building.Level == 0) continue;
            var definition = GameCatalog.Buildings[building.Type];
            if (definition.Phase != phase || definition.Recipe == null) continue;
            jobs.Add(new Job(city, building, definition));
        }

        var active = new List<Job>();
        foreach (var job in jobs)
        {
            var requirementsMet = job.Definition.Recipe!.Requirements.All(requirement =>
                realm.Resources[requirement.Key] >= requirement.Value * EconomyMath.LevelFactor(job.Building.Level));
            if (requirementsMet) active.Add(job);
            else report.Shortages.Add(job.Definition.Name + ": requisito de reserva no alcanzado");
        }

        var totalDemand = new Dictionary<ResourceType, decimal>();
        foreach (var job in active)
        foreach (var input in job.Definition.Recipe!.Inputs)
            Add(totalDemand, input.Key, input.Value * EconomyMath.LevelFactor(job.Building.Level));

        var availability = new Dictionary<ResourceType, decimal>();
        foreach (var demand in totalDemand)
            availability[demand.Key] = demand.Value <= 0m ? 1m : Math.Min(1m, realm.Resources[demand.Key] / demand.Value);

        foreach (var job in active)
        {
            var recipe = job.Definition.Recipe!;
            var factor = recipe.Inputs.Count == 0 ? 1m : recipe.Inputs.Min(input => availability[input.Key]);
            var levelFactor = EconomyMath.LevelFactor(job.Building.Level);
            if (factor < 0.999999m) report.Shortages.Add(job.Definition.Name + ": producción limitada al " + decimal.Round(factor * 100m, 1) + "%");

            foreach (var input in recipe.Inputs)
            {
                var amount = input.Value * levelFactor * factor;
                realm.Resources.TrySpend(input.Key, amount);
                Add(report.Consumed, input.Key, amount);
            }

            foreach (var output in recipe.Outputs)
            {
                var amount = output.Value * levelFactor * factor * ProductionMultiplier(realm, job.City, job.Building.Type, output.Key);
                realm.Resources.Add(output.Key, amount);
                Add(report.Produced, output.Key, amount);
            }
        }
    }

    private static decimal ProductionMultiplier(Realm realm, City city, BuildingType building, ResourceType resource)
    {
        decimal bonus = 0m;
        if (realm.Race == RaceType.Midgardsmenn && building == BuildingType.Market && resource == ResourceType.Silver) bonus += 0.25m;
        if (realm.Race == RaceType.Dvergar && resource == ResourceType.Iron) bonus += 0.10m;
        if (realm.Race == RaceType.Ljosalfar && resource == ResourceType.Grain) bonus += 0.10m;
        if (realm.Race == RaceType.Dokkalfar && resource == ResourceType.Tools) bonus += 0.10m;
        if (realm.Race == RaceType.Jotnar && resource == ResourceType.Stone) bonus += 0.10m;
        if (realm.Race == RaceType.Eldjotnar && resource == ResourceType.Charcoal) bonus += 0.10m;
        if (realm.Race == RaceType.Hrimthursar && resource == ResourceType.Water) bonus += 0.10m;

        if (resource == ResourceType.Water)
        {
            if (city.HasSpring) bonus += 0.05m;
            bonus += city.GetBuilding(BuildingType.Aqueduct).Level * 0.05m;
        }

        return 1m + bonus;
    }

    private static void Add(Dictionary<ResourceType, decimal> values, ResourceType resource, decimal amount)
    {
        if (values.ContainsKey(resource)) values[resource] += amount;
        else values[resource] = amount;
    }

    private sealed class Job
    {
        public Job(City city, CityBuilding building, BuildingDefinition definition)
        {
            City = city;
            Building = building;
            Definition = definition;
        }

        public City City { get; }
        public CityBuilding Building { get; }
        public BuildingDefinition Definition { get; }
    }
}
}
