# Wyrd

Wyrd es un prototipo de juego de estrategia offline inspirado en los juegos de navegador clásicos. Los reinos desarrollan ciudades, producen recursos, reclutan héroes y conquistan regiones para competir en distintos rankings.

La primera simulación jugable es un laboratorio económico de 90 días: permite elegir una raza vinculada a Yggdrasil, terminar días, producir y transformar 25 recursos, mejorar edificios y observar el Valor del Reino.

El [documento de diseño](docs/GAME_DESIGN.md) es la fuente de verdad para las decisiones del juego y se actualiza junto con el código.

## Estructura

- `src/Wyrd.Core`: dominio y reglas del juego en `netstandard2.1`, sin dependencias de Blazor ni Unity.
- `src/Wyrd.Web`: laboratorio Blazor WebAssembly.
- `tests/Wyrd.Core.Tests`: pruebas de las reglas económicas.
- `.github/workflows/deploy-pages.yml`: despliegue automático a GitHub Pages desde `main`.

## Desarrollo local

Requiere el SDK de .NET 8 o posterior.

```powershell
dotnet restore
dotnet test
dotnet run --project src/Wyrd.Web/Wyrd.Web.csproj
```

## Publicación

Cada push a `main` ejecuta el workflow de GitHub Pages. La aplicación se publica en:

https://gabra666.github.io/wyrd-blazor/
