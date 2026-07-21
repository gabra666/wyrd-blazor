using System;
using System.Collections.Generic;
using System.Linq;

namespace Wyrd.Core.Domain
{

public sealed class Kingdom
{
    private readonly List<City> _cities = new List<City>();
    private readonly List<Hero> _heroes = new List<Hero>();

    public Kingdom(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A kingdom needs a name.", nameof(name));
        }

        Name = name;
    }

    public string Name { get; }
    public IReadOnlyList<City> Cities => _cities;
    public IReadOnlyList<Hero> Heroes => _heroes;
    public int Score => (_cities.Count * 100) + _cities.Sum(city => city.Level * 25) + _heroes.Sum(hero => hero.Level * 10);

    public void AddCity(City city) => _cities.Add(city ?? throw new ArgumentNullException(nameof(city)));

    public void RecruitHero(Hero hero) => _heroes.Add(hero ?? throw new ArgumentNullException(nameof(hero)));
}
}
