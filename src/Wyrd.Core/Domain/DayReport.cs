using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class DayReport
{
    public DayReport(int day, decimal valueBefore)
    {
        Day = day;
        ValueBefore = valueBefore;
    }

    public int Day { get; }
    public decimal ValueBefore { get; }
    public decimal ValueAfter { get; set; }
    public Dictionary<ResourceType, decimal> Produced { get; } = new Dictionary<ResourceType, decimal>();
    public Dictionary<ResourceType, decimal> Consumed { get; } = new Dictionary<ResourceType, decimal>();
    public List<string> Shortages { get; } = new List<string>();
}
}
