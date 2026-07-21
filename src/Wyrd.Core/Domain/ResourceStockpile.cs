using System;
using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class ResourceStockpile
{
    private readonly Dictionary<ResourceType, int> _amounts = new Dictionary<ResourceType, int>();

    public int this[ResourceType resource] =>
        _amounts.TryGetValue(resource, out var amount) ? amount : 0;

    public IReadOnlyDictionary<ResourceType, int> Amounts => _amounts;

    public void Add(ResourceType resource, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The amount cannot be negative.");
        }

        _amounts[resource] = checked(this[resource] + amount);
    }

    public bool TrySpend(ResourceType resource, int amount)
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
}
}
