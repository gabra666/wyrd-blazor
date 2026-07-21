using System;

namespace Wyrd.Core.Domain
{

public sealed class Army
{
    public Army(int soldiers)
    {
        if (soldiers < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(soldiers));
        }

        Soldiers = soldiers;
    }

    public int Soldiers { get; private set; }

    public void Recruit(int soldiers)
    {
        if (soldiers <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(soldiers));
        }

        Soldiers = checked(Soldiers + soldiers);
    }
}
}
