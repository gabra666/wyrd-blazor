using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class ProductionRecipe
{
    public ProductionRecipe(
        IReadOnlyDictionary<ResourceType, decimal> inputs,
        IReadOnlyDictionary<ResourceType, decimal> outputs,
        IReadOnlyDictionary<ResourceType, decimal>? requirements = null)
    {
        Inputs = inputs;
        Outputs = outputs;
        Requirements = requirements ?? new Dictionary<ResourceType, decimal>();
    }

    public IReadOnlyDictionary<ResourceType, decimal> Inputs { get; }
    public IReadOnlyDictionary<ResourceType, decimal> Outputs { get; }
    public IReadOnlyDictionary<ResourceType, decimal> Requirements { get; }
}
}
