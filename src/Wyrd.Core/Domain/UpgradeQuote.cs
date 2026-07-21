using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class UpgradeQuote
{
    public UpgradeQuote(int targetLevel, decimal totalValue, IReadOnlyDictionary<ResourceType, decimal> costs, bool canUpgrade, string? blockedReason)
    {
        TargetLevel = targetLevel;
        TotalValue = totalValue;
        Costs = costs;
        CanUpgrade = canUpgrade;
        BlockedReason = blockedReason;
    }

    public int TargetLevel { get; }
    public decimal TotalValue { get; }
    public IReadOnlyDictionary<ResourceType, decimal> Costs { get; }
    public bool CanUpgrade { get; }
    public string? BlockedReason { get; }
}
}
