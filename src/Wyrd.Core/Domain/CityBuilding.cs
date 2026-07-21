using System;

namespace Wyrd.Core.Domain
{

public sealed class CityBuilding
{
    public CityBuilding(BuildingType type, int level = 0, decimal investedValue = 0)
    {
        if (level < 0 || level > 10) throw new ArgumentOutOfRangeException(nameof(level));
        Type = type;
        Level = level;
        InvestedValue = investedValue;
    }

    public BuildingType Type { get; }
    public int Level { get; private set; }
    public decimal InvestedValue { get; private set; }

    public void Upgrade(decimal investment)
    {
        if (Level >= 10) throw new InvalidOperationException("The building is already at maximum level.");
        if (investment < 0) throw new ArgumentOutOfRangeException(nameof(investment));
        Level++;
        InvestedValue += investment;
    }
}
}
