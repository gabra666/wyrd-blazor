using System.Collections.Generic;
using Wyrd.Core.Domain;

namespace Wyrd.Core.Rules
{

public static class GameCatalog
{
    public static readonly IReadOnlyDictionary<ResourceType, ResourceDefinition> Resources =
        new Dictionary<ResourceType, ResourceDefinition>
        {
            [ResourceType.Silver] = R(ResourceType.Silver, "Plata", ResourceCategory.Primary, 1m),
            [ResourceType.Water] = R(ResourceType.Water, "Agua", ResourceCategory.Primary, 0.05m),
            [ResourceType.Grain] = R(ResourceType.Grain, "Cereales", ResourceCategory.Primary, 0.10m),
            [ResourceType.Livestock] = R(ResourceType.Livestock, "Ganado", ResourceCategory.Primary, 1m),
            [ResourceType.Wood] = R(ResourceType.Wood, "Madera", ResourceCategory.Primary, 0.20m),
            [ResourceType.Iron] = R(ResourceType.Iron, "Hierro", ResourceCategory.Primary, 0.50m),
            [ResourceType.Stone] = R(ResourceType.Stone, "Piedra", ResourceCategory.Primary, 0.25m),
            [ResourceType.Food] = R(ResourceType.Food, "Comida", ResourceCategory.Processed, 0.10m),
            [ResourceType.Hides] = R(ResourceType.Hides, "Pieles y cuero", ResourceCategory.Processed, 0.60m),
            [ResourceType.Wool] = R(ResourceType.Wool, "Lana", ResourceCategory.Processed, 0.50m),
            [ResourceType.Planks] = R(ResourceType.Planks, "Tablones", ResourceCategory.Processed, 0.264m),
            [ResourceType.Charcoal] = R(ResourceType.Charcoal, "Carbón vegetal", ResourceCategory.Processed, 0.44m),
            [ResourceType.Tar] = R(ResourceType.Tar, "Alquitrán", ResourceCategory.Processed, 0.44m),
            [ResourceType.CutStone] = R(ResourceType.CutStone, "Piedra tallada", ResourceCategory.Processed, 0.44m),
            [ResourceType.Beer] = R(ResourceType.Beer, "Cerveza", ResourceCategory.Finished, 0.55m),
            [ResourceType.Tools] = R(ResourceType.Tools, "Herramientas", ResourceCategory.Finished, 1.507m),
            [ResourceType.Clothing] = R(ResourceType.Clothing, "Ropa", ResourceCategory.Finished, 1.21m),
            [ResourceType.Weapons] = R(ResourceType.Weapons, "Armas", ResourceCategory.Finished, 2.057m),
            [ResourceType.Shields] = R(ResourceType.Shields, "Escudos", ResourceCategory.Finished, 1.7908m),
            [ResourceType.Armor] = R(ResourceType.Armor, "Armaduras", ResourceCategory.Finished, 2.794m),
            [ResourceType.Jewelry] = R(ResourceType.Jewelry, "Joyas", ResourceCategory.Finished, 2.20m),
            [ResourceType.Whetstone] = R(ResourceType.Whetstone, "Piedras de afilar", ResourceCategory.Special, 25m),
            [ResourceType.Runestone] = R(ResourceType.Runestone, "Piedras rúnicas", ResourceCategory.Special, 25m),
            [ResourceType.Seidr] = R(ResourceType.Seidr, "Seiðr", ResourceCategory.Special, 1m),
            [ResourceType.Galdr] = R(ResourceType.Galdr, "Galdr", ResourceCategory.Special, 1m)
        };

    public static readonly IReadOnlyDictionary<RaceType, RaceDefinition> Races =
        new Dictionary<RaceType, RaceDefinition>
        {
            [RaceType.Midgardsmenn] = Race(RaceType.Midgardsmenn, "Miðgarðsmenn", "Midgard", "Humanos comerciantes y organizadores.", "+25% de plata del Mercado"),
            [RaceType.Dvergar] = Race(RaceType.Dvergar, "Dvergar", "Niðavellir", "Mineros y herreros de las profundidades.", "+10% de hierro"),
            [RaceType.Ljosalfar] = Race(RaceType.Ljosalfar, "Ljósálfar", "Álfheimr", "Elfos luminosos ligados a la fertilidad.", "+10% de cereales"),
            [RaceType.Dokkalfar] = Race(RaceType.Dokkalfar, "Dökkálfar", "Svartálfaheimr", "Artesanos de los dominios subterráneos.", "+10% de herramientas"),
            [RaceType.Jotnar] = Race(RaceType.Jotnar, "Jötnar", "Jötunheimr", "Gigantes ligados a las fuerzas de la tierra.", "+10% de piedra"),
            [RaceType.Eldjotnar] = Race(RaceType.Eldjotnar, "Eldjötnar", "Múspellsheimr", "Gigantes de fuego y maestros de la combustión.", "+10% de carbón vegetal"),
            [RaceType.Hrimthursar] = Race(RaceType.Hrimthursar, "Hrímþursar", "Niflheim", "Gigantes nacidos de la escarcha primordial.", "+10% de agua")
        };

    public static readonly IReadOnlyDictionary<BuildingType, BuildingDefinition> Buildings = CreateBuildings();

    private static IReadOnlyDictionary<BuildingType, BuildingDefinition> CreateBuildings()
    {
        var buildings = new Dictionary<BuildingType, BuildingDefinition>();

        Add(buildings, BuildingType.SilverMine, "Mina de plata", "Extracción", 1, Output(ResourceType.Silver, 10m));
        Add(buildings, BuildingType.Well, "Pozo", "Extracción", 1, Output(ResourceType.Water, 200m));
        Add(buildings, BuildingType.Fields, "Campos", "Extracción", 1, Output(ResourceType.Grain, 100m));
        Add(buildings, BuildingType.Pastures, "Pastos", "Extracción", 1, Output(ResourceType.Livestock, 10m));
        Add(buildings, BuildingType.Woodcutters, "Leñadores", "Extracción", 1, Output(ResourceType.Wood, 50m));
        Add(buildings, BuildingType.IronMine, "Mina de hierro", "Extracción", 1, Output(ResourceType.Iron, 20m));
        Add(buildings, BuildingType.Quarry, "Cantera", "Extracción", 1, Output(ResourceType.Stone, 40m));
        Add(buildings, BuildingType.Market, "Mercado", "Comercio", 1,
            Recipe(null, D(ResourceType.Silver, 4m, ResourceType.Whetstone, 0.10m, ResourceType.Runestone, 0.05m)));
        buildings[BuildingType.Aqueduct] = new BuildingDefinition(BuildingType.Aqueduct, "Acueducto", "Agua", 0, null,
            new[] { BuildingType.Well }, "+5% a la producción de agua por nivel");

        Add(buildings, BuildingType.Mill, "Molino", "Alimentos", 2,
            Recipe(D(ResourceType.Grain, 50m), D(ResourceType.Food, 55m)), BuildingType.Fields);
        Add(buildings, BuildingType.Slaughterhouse, "Matadero", "Alimentos", 2,
            Recipe(D(ResourceType.Livestock, 8m), D(ResourceType.Food, 40m, ResourceType.Hides, 8m)), BuildingType.Pastures);
        Add(buildings, BuildingType.ShearingShed, "Esquiladero", "Alimentos", 2,
            new ProductionRecipe(Empty(), D(ResourceType.Wool, 10m), D(ResourceType.Livestock, 1m)), BuildingType.Pastures);
        Add(buildings, BuildingType.Sawmill, "Aserradero", "Madera", 2,
            Recipe(D(ResourceType.Wood, 12m), D(ResourceType.Planks, 10m)), BuildingType.Woodcutters);
        Add(buildings, BuildingType.CharcoalKiln, "Carbonera", "Madera", 2,
            Recipe(D(ResourceType.Wood, 8m), D(ResourceType.Charcoal, 4m)), BuildingType.Woodcutters);
        Add(buildings, BuildingType.TarKiln, "Horno de alquitrán", "Madera", 2,
            Recipe(D(ResourceType.Wood, 4m), D(ResourceType.Tar, 2m)), BuildingType.Woodcutters);
        Add(buildings, BuildingType.Stonemason, "Cantería", "Piedra", 2,
            Recipe(D(ResourceType.Stone, 32m), D(ResourceType.CutStone, 20m)), BuildingType.Quarry);

        Add(buildings, BuildingType.Brewery, "Cervecería", "Manufactura", 3,
            Recipe(D(ResourceType.Grain, 30m, ResourceType.Water, 40m), D(ResourceType.Beer, 10m)), BuildingType.Fields, BuildingType.Well);
        Add(buildings, BuildingType.Smithy, "Herrería", "Manufactura", 3,
            Recipe(D(ResourceType.Iron, 3m, ResourceType.Wood, 4m, ResourceType.Charcoal, 1m), D(ResourceType.Tools, 2m)), BuildingType.IronMine, BuildingType.Woodcutters, BuildingType.CharcoalKiln);
        Add(buildings, BuildingType.WeaponForge, "Forja de armas", "Militar", 3,
            Recipe(D(ResourceType.Iron, 5m, ResourceType.Wood, 4m, ResourceType.Charcoal, 1m), D(ResourceType.Weapons, 2m)), BuildingType.IronMine, BuildingType.Woodcutters, BuildingType.CharcoalKiln);
        Add(buildings, BuildingType.ShieldWorkshop, "Taller de escudos", "Militar", 3,
            Recipe(D(ResourceType.Iron, 2m, ResourceType.Planks, 4m, ResourceType.Hides, 2m), D(ResourceType.Shields, 2m)), BuildingType.IronMine, BuildingType.Sawmill, BuildingType.Slaughterhouse);
        Add(buildings, BuildingType.Armory, "Armería", "Militar", 3,
            Recipe(D(ResourceType.Iron, 6m, ResourceType.Hides, 2m, ResourceType.Charcoal, 2m), D(ResourceType.Armor, 2m)), BuildingType.IronMine, BuildingType.Slaughterhouse, BuildingType.CharcoalKiln);
        Add(buildings, BuildingType.Tailor, "Sastrería", "Manufactura", 3,
            Recipe(D(ResourceType.Wool, 4m, ResourceType.Hides, 4m), D(ResourceType.Clothing, 4m)), BuildingType.ShearingShed, BuildingType.Slaughterhouse);
        Add(buildings, BuildingType.Silversmith, "Orfebrería", "Manufactura", 3,
            Recipe(D(ResourceType.Silver, 4m), D(ResourceType.Jewelry, 2m)), BuildingType.SilverMine);
        Add(buildings, BuildingType.Temple, "Templo", "Místico", 3, Output(ResourceType.Galdr, 10m));
        Add(buildings, BuildingType.MeditationCircle, "Círculo de Meditación", "Místico", 3, Output(ResourceType.Seidr, 10m));

        return buildings;
    }

    private static void Add(Dictionary<BuildingType, BuildingDefinition> target, BuildingType type, string name, string group, int phase, ProductionRecipe recipe, params BuildingType[] prerequisites)
        => target[type] = new BuildingDefinition(type, name, group, phase, recipe, prerequisites);

    private static ResourceDefinition R(ResourceType type, string name, ResourceCategory category, decimal value)
        => new ResourceDefinition(type, name, category, value);

    private static RaceDefinition Race(RaceType type, string name, string realm, string description, string bonus)
        => new RaceDefinition(type, name, realm, description, bonus);

    private static ProductionRecipe Output(ResourceType resource, decimal amount) => Recipe(null, D(resource, amount));

    private static ProductionRecipe Recipe(IReadOnlyDictionary<ResourceType, decimal>? inputs, IReadOnlyDictionary<ResourceType, decimal> outputs)
        => new ProductionRecipe(inputs ?? Empty(), outputs);

    private static ProductionRecipe Recipe(IReadOnlyDictionary<ResourceType, decimal> inputs, IReadOnlyDictionary<ResourceType, decimal> outputs, params BuildingType[] unused)
        => new ProductionRecipe(inputs, outputs);

    private static Dictionary<ResourceType, decimal> Empty() => new Dictionary<ResourceType, decimal>();

    private static Dictionary<ResourceType, decimal> D(ResourceType a, decimal av)
        => new Dictionary<ResourceType, decimal> { [a] = av };

    private static Dictionary<ResourceType, decimal> D(ResourceType a, decimal av, ResourceType b, decimal bv)
        => new Dictionary<ResourceType, decimal> { [a] = av, [b] = bv };

    private static Dictionary<ResourceType, decimal> D(ResourceType a, decimal av, ResourceType b, decimal bv, ResourceType c, decimal cv)
        => new Dictionary<ResourceType, decimal> { [a] = av, [b] = bv, [c] = cv };
}
}
