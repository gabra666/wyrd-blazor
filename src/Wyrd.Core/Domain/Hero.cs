using System;

namespace Wyrd.Core.Domain
{

public sealed class Hero
{
    public Hero(string name, Army army)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A hero needs a name.", nameof(name));
        }

        Name = name;
        Army = army ?? throw new ArgumentNullException(nameof(army));
    }

    public string Name { get; }
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; }
    public Army Army { get; }

    public void GainExperience(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Experience = checked(Experience + amount);
    }
}
}
