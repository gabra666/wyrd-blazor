using System.Collections.Generic;
using System.Linq;
using Wyrd.Core.Domain;

namespace Wyrd.Core.Rules
{

public sealed class BuildingUpgradeService
{
    public UpgradeQuote GetQuote(Realm realm, City city, BuildingType type)
    {
        var building = city.GetBuilding(type);
        var targetLevel = building.Level + 1;
        if (targetLevel > 10)
            return new UpgradeQuote(targetLevel, 0m, new Dictionary<ResourceType, decimal>(), false, "Nivel máximo alcanzado");

        var total = EconomyMath.UpgradeValue(targetLevel);
        var costs = BuildCosts(targetLevel, total);
        var definition = GameCatalog.Buildings[type];

        if (definition.Prerequisites.Count > 0)
        {
            var maximum = definition.Prerequisites.Min(prerequisite => city.GetBuilding(prerequisite).Level);
            if (targetLevel > maximum)
            {
                var names = definition.Prerequisites
                    .Where(prerequisite => city.GetBuilding(prerequisite).Level < targetLevel)
                    .Select(prerequisite => GameCatalog.Buildings[prerequisite].Name);
                return new UpgradeQuote(targetLevel, total, costs, false, "Requiere nivel " + targetLevel + " en " + string.Join(", ", names));
            }
        }

        if (!realm.Resources.CanAfford(costs))
        {
            var missing = costs
                .Where(cost => realm.Resources[cost.Key] < cost.Value)
                .Select(cost => GameCatalog.Resources[cost.Key].Name);
            return new UpgradeQuote(targetLevel, total, costs, false, "Faltan: " + string.Join(", ", missing));
        }

        return new UpgradeQuote(targetLevel, total, costs, true, null);
    }

    public bool TryUpgrade(Realm realm, City city, BuildingType type)
    {
        var quote = GetQuote(realm, city, type);
        if (!quote.CanUpgrade || !realm.Resources.TrySpend(quote.Costs)) return false;
        city.GetBuilding(type).Upgrade(quote.TotalValue);
        return true;
    }

    private static IReadOnlyDictionary<ResourceType, decimal> BuildCosts(int level, decimal total)
    {
        var shares = level <= 3
            ? new Dictionary<ResourceType, decimal> { [ResourceType.Silver] = 0.40m, [ResourceType.Wood] = 0.30m, [ResourceType.Stone] = 0.30m }
            : level <= 6
                ? new Dictionary<ResourceType, decimal> { [ResourceType.Silver] = 0.35m, [ResourceType.Planks] = 0.25m, [ResourceType.CutStone] = 0.25m, [ResourceType.Tools] = 0.15m }
                : new Dictionary<ResourceType, decimal> { [ResourceType.Silver] = 0.30m, [ResourceType.Planks] = 0.25m, [ResourceType.CutStone] = 0.25m, [ResourceType.Tools] = 0.15m, [ResourceType.Tar] = 0.05m };

        var costs = new Dictionary<ResourceType, decimal>();
        foreach (var share in shares)
            costs[share.Key] = total * share.Value / GameCatalog.Resources[share.Key].UnitValue;
        return costs;
    }
}
}
