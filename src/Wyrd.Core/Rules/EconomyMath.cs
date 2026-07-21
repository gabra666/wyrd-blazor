namespace Wyrd.Core.Rules
{

public static class EconomyMath
{
    public static decimal LevelFactor(int level)
    {
        if (level <= 0) return 0m;
        decimal result = 1m;
        for (var i = 1; i < level; i++) result *= 1.25m;
        return result;
    }

    public static decimal UpgradeValue(int targetLevel)
    {
        if (targetLevel < 1 || targetLevel > 10) return 0m;
        decimal result = 50m;
        for (var i = 1; i < targetLevel; i++) result *= 1.25m;
        return result;
    }
}
}
