using System;

namespace Wyrd.Core.Domain
{

public sealed class Region
{
    public Region(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A region needs an id and a name.");
        }

        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; }
}
}
