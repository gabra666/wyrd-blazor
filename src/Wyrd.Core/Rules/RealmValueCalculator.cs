using Wyrd.Core.Domain;

namespace Wyrd.Core.Rules
{

public sealed class RealmValueCalculator
{
    public decimal Calculate(Realm realm) => CityValue(realm) + ResourceValue(realm) + BuildingValue(realm);

    public decimal CityValue(Realm realm) => realm.Cities.Count * 100m;

    public decimal ResourceValue(Realm realm)
    {
        decimal total = 0m;
        foreach (var resource in GameCatalog.Resources.Values)
        {
            total += realm.Resources[resource.Type] * resource.UnitValue;
        }
        return total;
    }

    public decimal BuildingValue(Realm realm)
    {
        decimal total = 0m;
        foreach (var city in realm.Cities)
        foreach (var building in city.Buildings.Values)
            total += building.InvestedValue * 0.75m;
        return total;
    }
}
}
