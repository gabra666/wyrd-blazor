using System.Collections.Generic;
using Wyrd.Core.Domain;

namespace Wyrd.Core.Rules
{

public static class GameRules
{
    private static readonly IReadOnlyDictionary<ResourceType, int> IncomePerTurn =
        new Dictionary<ResourceType, int>
        {
            [ResourceType.Gold] = 120,
            [ResourceType.Wood] = 80,
            [ResourceType.Stone] = 60,
            [ResourceType.Food] = 100
        };

    public static void ApplyCityIncome(City city)
    {
        foreach (var income in IncomePerTurn)
        {
            city.Resources.Add(income.Key, income.Value * city.Level);
        }
    }
}
}
