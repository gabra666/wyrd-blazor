using System;
using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class BuildingDefinition
{
    public BuildingDefinition(
        BuildingType type,
        string name,
        string group,
        int phase,
        ProductionRecipe? recipe = null,
        IReadOnlyList<BuildingType>? prerequisites = null,
        string? effectDescription = null)
    {
        Type = type;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Group = group ?? throw new ArgumentNullException(nameof(group));
        Phase = phase;
        Recipe = recipe;
        Prerequisites = prerequisites ?? new BuildingType[0];
        EffectDescription = effectDescription;
    }

    public BuildingType Type { get; }
    public string Name { get; }
    public string Group { get; }
    public int Phase { get; }
    public ProductionRecipe? Recipe { get; }
    public IReadOnlyList<BuildingType> Prerequisites { get; }
    public string? EffectDescription { get; }
}
}
