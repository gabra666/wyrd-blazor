using System;
using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class ResourceStockpile
{
    private readonly Dictionary<ResourceType, decimal> _amounts = new Dictionary<ResourceType, decimal>();

    public decimal this[ResourceType resource] =>
        _amounts.TryGetValue(resource, out var amount) ? amount : 0;

    public IReadOnlyDictionary<ResourceType, decimal> Amounts => _amounts;

    public void Add(ResourceType resource, decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The amount cannot be negative.");
        }

        _amounts[resource] = this[resource] + amount;
    }

    public bool TrySpend(ResourceType resource, decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The amount cannot be negative.");
        }

        if (this[resource] < amount)
        {
            return false;
        }

        _amounts[resource] -= amount;
        return true;
    }

    public bool CanAfford(IReadOnlyDictionary<ResourceType, decimal> costs)
    {
        foreach (var cost in costs)
        {
            if (this[cost.Key] < cost.Value)
            {
                return false;
            }
        }

        return true;
    }

    public bool TrySpend(IReadOnlyDictionary<ResourceType, decimal> costs)
    {
        if (!CanAfford(costs))
        {
            return false;
        }

        foreach (var cost in costs)
        {
            _amounts[cost.Key] = this[cost.Key] - cost.Value;
        }

        return true;
    }
}
}
