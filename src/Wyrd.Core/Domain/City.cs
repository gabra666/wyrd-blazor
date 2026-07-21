using System;

namespace Wyrd.Core.Domain
{

public sealed class City
{
    public City(string name, string regionId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A city needs a name.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(regionId))
        {
            throw new ArgumentException("A city needs a region.", nameof(regionId));
        }

        Id = Guid.NewGuid();
        Name = name;
        RegionId = regionId;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string RegionId { get; }
    public int Level { get; private set; } = 1;
    public int WallLevel { get; private set; } = 1;
    public ResourceStockpile Resources { get; } = new ResourceStockpile();

    public void ImproveCity() => Level = checked(Level + 1);

    public void ImproveWalls() => WallLevel = checked(WallLevel + 1);
}
}
