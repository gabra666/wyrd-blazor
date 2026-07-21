namespace Wyrd.Core.Domain
{

public sealed class RealmValuePoint
{
    public RealmValuePoint(int day, decimal value)
    {
        Day = day;
        Value = value;
    }

    public int Day { get; }
    public decimal Value { get; }
}
}
