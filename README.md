# Wyrd

Wyrd es un prototipo de juego de estrategia inspirado en los juegos de navegador clásicos: los reinos desarrollan ciudades, producen recursos, reclutan héroes y conquistan regiones para competir por el primer puesto del ranking.

Este repositorio prioriza la iteración rápida de reglas y mecánicas en el navegador. La lógica del juego vive separada de la interfaz para que pueda reutilizarse más adelante desde Unity.

El [documento de diseño](docs/GAME_DESIGN.md) recoge la visión del juego, las decisiones establecidas, las propuestas provisionales y las cuestiones todavía abiertas. Es un documento vivo y debe actualizarse junto con las reglas del proyecto.

## Estructura

- `src/Wyrd.Core`: dominio y reglas del juego, sin dependencias de Blazor. Apunta a `netstandard2.1` para facilitar su reutilización en Unity.
- `src/Wyrd.Web`: cliente Blazor WebAssembly usado como laboratorio interactivo.
- `.github/workflows/deploy-pages.yml`: compilación y despliegue automático en GitHub Pages desde `main`.
- `docs/GAME_DESIGN.md`: documento vivo de diseño y registro de decisiones.
- `AGENTS.md`: instrucciones para mantener sincronizados diseño, terminología y código.

## Desarrollo local

Requiere el SDK de .NET 8 o posterior.

```powershell
dotnet restore
dotnet run --project src/Wyrd.Web/Wyrd.Web.csproj
```

## Publicación

Cada push a `main` ejecuta el workflow de GitHub Pages. En la configuración del repositorio, selecciona **GitHub Actions** como fuente de Pages si todavía no está habilitada.

La aplicación quedará disponible en `https://gabra666.github.io/wyrd-blazor/` cuando termine el primer despliegue.
