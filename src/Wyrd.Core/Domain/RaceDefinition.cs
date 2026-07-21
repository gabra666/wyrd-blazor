using System;

namespace Wyrd.Core.Domain
{

public sealed class RaceDefinition
{
    public RaceDefinition(RaceType type, string name, string realm, string description, string bonusDescription)
    {
        Type = type;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Realm = realm ?? throw new ArgumentNullException(nameof(realm));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        BonusDescription = bonusDescription ?? throw new ArgumentNullException(nameof(bonusDescription));
    }

    public RaceType Type { get; }
    public string Name { get; }
    public string Realm { get; }
    public string Description { get; }
    public string BonusDescription { get; }
}
}
