using System;
using System.Collections.Generic;

namespace Wyrd.Core.Domain
{

public sealed class Realm
{
    private readonly List<City> _cities = new List<City>();
    private readonly List<RealmValuePoint> _valueHistory = new List<RealmValuePoint>();

    public Realm(string name, RaceType race, int maximumDays = 90)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A realm needs a name.", nameof(name));
        if (maximumDays < 1) throw new ArgumentOutOfRangeException(nameof(maximumDays));
        Name = name;
        Race = race;
        MaximumDays = maximumDays;
    }

    public string Name { get; }
    public RaceType Race { get; }
    public ResourceStockpile Resources { get; } = new ResourceStockpile();
    public IReadOnlyList<City> Cities => _cities;
    public IReadOnlyList<RealmValuePoint> ValueHistory => _valueHistory;
    public int CurrentDay { get; private set; } = 1;
    public int MaximumDays { get; }
    public bool IsComplete { get; private set; }
    public DayReport? LastReport { get; internal set; }

    public void AddCity(City city) => _cities.Add(city ?? throw new ArgumentNullException(nameof(city)));
    public void RecordValue(int day, decimal value) => _valueHistory.Add(new RealmValuePoint(day, value));

    internal void CompleteDay()
    {
        if (CurrentDay >= MaximumDays) IsComplete = true;
        else CurrentDay++;
    }
}
}
