using System;
using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class City
{
    private readonly Dictionary<BuildingType, CityBuilding> _buildings = new Dictionary<BuildingType, CityBuilding>();

    public City(string name, string regionId, bool hasSpring = false)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A city needs a name.", nameof(name));
        if (string.IsNullOrWhiteSpace(regionId)) throw new ArgumentException("A city needs a region.", nameof(regionId));
        Id = Guid.NewGuid();
        Name = name;
        RegionId = regionId;
        HasSpring = hasSpring;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string RegionId { get; }
    public bool HasSpring { get; }
    public IReadOnlyDictionary<BuildingType, CityBuilding> Buildings => _buildings;

    public CityBuilding GetBuilding(BuildingType type) => _buildings[type];
    public void AddBuilding(CityBuilding building) => _buildings.Add(building.Type, building);
}
}
