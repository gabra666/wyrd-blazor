using System;

namespace Wyrd.Core.Domain
{

public sealed class ResourceDefinition
{
    public ResourceDefinition(ResourceType type, string name, ResourceCategory category, decimal unitValue)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A resource needs a name.", nameof(name));
        if (unitValue < 0) throw new ArgumentOutOfRangeException(nameof(unitValue));

        Type = type;
        Name = name;
        Category = category;
        UnitValue = unitValue;
    }

    public ResourceType Type { get; }
    public string Name { get; }
    public ResourceCategory Category { get; }
    public decimal UnitValue { get; }
}
}
