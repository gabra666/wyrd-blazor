# Project Wyrd — documento de diseño

> Documento vivo. Debe actualizarse cuando una conversación, prototipo o prueba de balance produzca una decisión nueva o cambie una existente.

| Campo | Valor |
| --- | --- |
| Estado | Preproducción y prototipado de reglas |
| Versión | 0.3 |
| Última revisión | 2026-07-23 |
| Plataforma de prototipado | Blazor WebAssembly |
| Presentación final prevista | Unity |

## Cómo mantener este documento

- Las reglas acordadas se incorporan a su sección y a **Decisiones establecidas**.
- Las cifras que aún deben validarse se marcan como **provisionales**.
- Las propuestas descartadas se conservan en el historial con su motivo.
- El código, las pruebas y este documento deben emplear la misma terminología.
- En Wyrd se habla de **reinos** y **Valor del Reino**, nunca de imperios ni `Empire Value`.

## Historial de revisiones

| Versión | Fecha | Cambio |
| --- | --- | --- |
| 0.1 | 2026-07-21 | Primera consolidación del concepto y los sistemas previstos. |
| 0.2 | 2026-07-22 | Se define el primer laboratorio económico: siete razas, 25 recursos, cadenas productivas, niveles de edificios, paso del día y Valor del Reino. Se sustituye “imperio” por “reino”. |
| 0.3 | 2026-07-23 | Se fija una campaña de 90 cambios de día, 50 habitantes fundadores y un máximo de 25.000 habitantes por ciudad. Se incorpora como propuesta la primera curva demográfica. |

## 1. Concepto central

Project Wyrd es un juego de estrategia offline para un solo jugador inspirado en *Empire Strike* y ambientado en una fantasía basada en la mitología nórdica.

Una campaña completa enfrentará al reino del jugador con 99 reinos controlados por IA. No existen servidores, jugadores reales ni presión de tiempo: el mundo avanza cuando el jugador decide terminar el día.

Los pilares previstos son:

- Construcción y administración de ciudades.
- Producción, transformación y acumulación de recursos.
- Crecimiento de población y reclutamiento de tropas.
- Héroes que lideran ejércitos.
- Conquista de ciudades y control regional.
- Clanes, guerras, magia global y rankings.

> Wyrd es un juego de estrategia offline donde el jugador desarrolla un reino en un mundo nórdico simulado junto a 99 reinos de IA. Su lógica reside en un núcleo C# independiente, reutilizable desde el laboratorio web y desde una presentación final en Unity.

## 2. Campaña y paso del día

El jugador actúa sin límite de tiempo real y pulsa **Terminar día** para resolver el mundo. En el juego completo ese paso hará avanzar población, recursos, construcciones, entrenamiento, movimientos, ataques, eventos, rankings y control territorial.

Una campaña dura **90 cambios de día**. El día es la unidad de turno controlada por el jugador; su equivalencia exacta con el tiempo del mundo todavía debe definirse.

Cada día económico se resuelve en orden:

1. Materias primas.
2. Recursos procesados.
3. Productos terminados y recursos especiales.

Un recurso producido en una fase puede consumirse durante una fase posterior del mismo día.

## 3. Reinos y ciudades

Los recursos pertenecen al reino y forman un inventario global compartido por todas sus ciudades. Las ciudades son los núcleos territoriales y productivos; más adelante también contendrán población, defensas, tropas y un héroe asignado.

El primer escenario contiene el reino **El Pacto del Cuervo** y su ciudad **Skallgard**, situada en los Confines del Norte. Skallgard posee un Manantial que aumenta un 5% su producción de agua.

Una ciudad recién fundada comienza como un asentamiento de **50 habitantes** y nunca puede superar los **25.000 habitantes**. Su crecimiento debe ser rápido en términos proporcionales durante las primeras etapas y reducirse al acercarse al límite. Los edificios no producen habitantes directamente: proporcionan agua, alimentos, vivienda, trabajo, seguridad y otros factores que permiten sostener y atraer población.

Como primera propuesta de balance, todavía provisional, se estudiará una curva de Gompertz:

`crecimiento base = 0,056 × población × ln(25.000 / población)`

Esta expresión representa el cambio demográfico total, incluida principalmente la inmigración. La capacidad efectiva de agua, alimentos y vivienda deberá limitar el crecimiento real, por lo que una ciudad solo se acercará a 25.000 habitantes si el jugador amplía continuamente su infraestructura.

El control regional se decidirá por el clan que posea más ciudades en una región. Por tanto, conquistar una ciudad puede cambiar tanto los recursos disponibles como el gobierno regional.

## 4. Razas de Yggdrasil

El laboratorio incluye siete razas jugables. Cada una comienza con una única ventaja económica; las ventajas militares, mágicas, demográficas y territoriales se añadirán en capas posteriores.

| Raza | Mundo vinculado | Ventaja económica inicial |
| --- | --- | --- |
| Miðgarðsmenn | Midgard | +25% de plata producida por el Mercado |
| Dvergar | Niðavellir | +10% de hierro |
| Ljósálfar | Álfheimr | +10% de cereales |
| Dökkálfar | Svartálfaheimr | +10% de herramientas |
| Jötnar | Jötunheimr | +10% de piedra |
| Eldjötnar | Múspellsheimr | +10% de carbón vegetal |
| Hrímþursar | Niflheim | +10% de agua |

Los Æsir y Vanir pueden aparecer como entidades divinas o facciones superiores, pero no son razas jugables normales. Hel no aporta por ahora una raza jugable; podrá utilizarse para eventos, criaturas o facciones sobrenaturales. Los draugar no forman una civilización jugable.

La separación entre Dvergar y Dökkálfar es una interpretación propia del mundo de Wyrd.

## 5. Recursos

El reino puede almacenar 25 recursos.

### Materias primas

- Plata: moneda, comercio, joyería y mejoras.
- Agua: población, alimentos y cerveza.
- Cereales: comida y cerveza.
- Ganado: comida, pieles y lana.
- Madera: combustible, herramientas y componentes.
- Hierro: herramientas y equipamiento militar.
- Piedra: construcción y piedra tallada.

### Recursos procesados

- Comida.
- Pieles y cuero, representados como un único recurso.
- Lana.
- Tablones.
- Carbón vegetal.
- Alquitrán.
- Piedra tallada.

### Productos terminados

- Cerveza.
- Herramientas.
- Ropa.
- Armas.
- Escudos.
- Armaduras.
- Joyas.

### Recursos especiales

- Piedras de afilar: entrenamiento de habilidades para `Warrior` y `Thief`.
- Piedras rúnicas: entrenamiento de habilidades para `Mage` y `Priest`.
- Seiðr: magia asociada principalmente a magos.
- Galdr: magia asociada principalmente a sacerdotes.

Las piedras de afilar no se fabrican con piedra común. El Mercado representa su importación gradual y, cuando exista combate, también podrán obtenerse como botín. Se descartan los objetos rituales como recurso económico genérico; los futuros artefactos serán objetos concretos.

## 6. Edificios y cadenas productivas

Cada ciudad tiene un edificio de cada tipo, con niveles de 0 a 10. Nivel 0 significa que aún no se ha construido. Las mejoras se completan inmediatamente.

Productores y soportes:

- Mina de plata, Pozo, Campos, Pastos, Leñadores, Mina de hierro y Cantera.
- Mercado: produce plata y progreso gradual de piedras de afilar y rúnicas.
- Acueducto: aumenta un 5% la producción de agua por nivel.
- Templo: produce Galdr.
- Círculo de Meditación: produce Seiðr.

Transformadores:

- Molino: cereales → comida.
- Matadero: ganado → comida y pieles.
- Esquiladero: requiere una reserva de ganado, sin consumirla → lana.
- Aserradero: madera → tablones.
- Carbonera: madera → carbón vegetal.
- Horno de alquitrán: madera → alquitrán.
- Cantería: piedra → piedra tallada.
- Cervecería: cereales y agua → cerveza.
- Herrería: hierro, madera y carbón → herramientas.
- Forja de armas: hierro, madera y carbón → armas.
- Taller de escudos: hierro, tablones y pieles → escudos.
- Armería: hierro, pieles y carbón → armaduras.
- Sastrería: lana y pieles → ropa.
- Orfebrería: plata → joyas.

La madera sin procesar se usa en mangos, herramientas y armas. Los tablones se reservan para escudos y construcciones más elaboradas.

Un procesador no puede superar el menor nivel de los edificios que producen directamente sus ingredientes. Los modificadores pasivos y el Mercado no cuentan para este límite.

## 7. Producción y escasez

La producción y el consumo de una receta crecen un **25% compuesto por nivel**:

`cantidad del nivel = cantidad base × 1,25^(nivel − 1)`

Los procesadores trabajan automáticamente cuando disponen de ingredientes. Si varios edificios de una misma fase solicitan un recurso insuficiente, el inventario disponible se reparte proporcionalmente. Una receta con varios ingredientes queda limitada por el ingrediente que cubra el menor porcentaje de su demanda.

Las ventajas raciales, el Manantial y el Acueducto se suman antes de multiplicar la producción correspondiente.

El balance aspira a conservar al menos un 20% de excedente normal de materias primas y a añadir un 10% de valor en cada transformación. Las cantidades concretas del catálogo actual son valores iniciales de experimentación y deberán validarse jugando el laboratorio.

## 8. Mejoras y Valor del Reino

El coste provisional de alcanzar un nivel es:

`50 × 1,25^(nivel objetivo − 1)` puntos de valor.

Composición provisional:

- Niveles 1–3: 40% plata, 30% madera y 30% piedra.
- Niveles 4–6: 35% plata, 25% tablones, 25% piedra tallada y 15% herramientas.
- Niveles 7–10: 30% plata, 25% tablones, 25% piedra tallada, 15% herramientas y 5% alquitrán.

El Valor del Reino se calcula como:

`100 por ciudad + valor de los recursos almacenados + 75% de la inversión acumulada en edificios`

Al gastar recursos en una mejora, el edificio conserva el 75% de ese valor. Por ello, el Valor del Reino baja inmediatamente un 25% del coste y después puede recuperarse mediante la producción adicional.

Los valores unitarios y recetas exactas están implementados en `GameCatalog` como parámetros provisionales de balance. La interfaz muestra cantidad, variación diaria, valor unitario y contribución total para facilitar su ajuste.

## 9. Héroes, tropas y combate futuro

Las cuatro clases principales de héroe serán `Warrior`, `Thief`, `Mage` y `Priest`. Los héroes liderarán ejércitos y tendrán ataque, defensa, daño, velocidad, moral, vida, energía y heridas.

Se estudia una progresión de 20 tipos de tropas, con nuevos escalones ligados a la población. El combate se resolverá por rondas y considerará héroes, tropas, moral, terreno, defensas, velocidad y composición del ejército.

Los valores aún provisionales incluyen un punto de atributo por cada ocho victorias y acceso a magia global importante alrededor del nivel 12.

## 10. Puntos de acción, clanes y regiones

Los puntos de acción podrán acumularse. Servirán para construir, entrenar, mover ejércitos, atacar, usar habilidades y realizar acciones diplomáticas. La referencia inicial es 100 puntos diarios más bonificaciones, aún sin validar.

Los límites de ataque serán independientes de los puntos de acción para impedir ofensivas ilimitadas durante un único día.

Los reinos podrán formar clanes, declarar guerras, coordinar ataques, emplear magia global y controlar regiones por mayoría de ciudades. La fama, los monumentos y los rankings especializados se desarrollarán después del núcleo económico.

## 11. Arquitectura técnica

- `Wyrd.Core`: modelos, reglas y simulación sin dependencias de Blazor ni Unity.
- `Wyrd.Web`: laboratorio interactivo para ejecutar días, observar cadenas y ajustar balance.
- Unity: futura visualización 2D/3D, animación, audio, mapas, efectos e interfaz final.

Las relaciones de IA pertenecerán al núcleo. LOVE/HATE podrá complementar la presentación o el comportamiento en Unity, pero no será una dependencia del simulador.

## 12. Decisiones establecidas

- Juego offline para un solo jugador con avance manual por días.
- Campañas de 90 cambios de día.
- Mundo final de un reino jugador y 99 reinos de IA.
- Terminología `Realm`, Reino y Valor del Reino.
- Inventario global del reino.
- 50 habitantes al fundar una ciudad y máximo de 25.000 habitantes por ciudad.
- Crecimiento demográfico rápido proporcionalmente al principio y lento al acercarse al máximo.
- Siete razas jugables vinculadas a Yggdrasil.
- 25 recursos agrupados en cuatro etapas.
- Un edificio de cada tipo por ciudad, niveles 0–10 y mejora inmediata.
- Producción compuesta del 25% por nivel.
- Fases productivas dentro del mismo día y escasez proporcional.
- Conservación del 75% del valor invertido en edificios.
- Núcleo C# independiente y Blazor como laboratorio.

## 13. Valores provisionales y cuestiones abiertas

Valores por validar en el laboratorio:

- Producciones, consumos y valores unitarios actuales.
- Coste y composición de las mejoras.
- Excedente objetivo del 20% y valor añadido del 10%.
- Fórmula demográfica de Gompertz y coeficiente inicial de 0,056.
- Umbrales de asentamiento, aldea, pueblo, ciudad y metrópolis.
- Correspondencia entre un cambio de día y el tiempo transcurrido dentro del mundo.

Sistemas todavía abiertos:

- Modificadores demográficos y consumo de comida, agua, ropa y cerveza.
- Tabla definitiva de tropas y razas en combate.
- Límites diarios de ataques y puntos de acción.
- Personalidades, diplomacia y decisiones de la IA.
- Profundidad del combate y fórmula de rankings.
- Fama, monumentos, magia global y afinidades zodiacales.
- Guardado de partida, regiones y mapas definitivos.

La siguiente iteración debe utilizar los datos obtenidos en el laboratorio para ajustar costes y producciones antes de añadir población o IA.
