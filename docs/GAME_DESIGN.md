# Project Wyrd — documento de diseño

> Documento vivo. Debe actualizarse cuando una conversación, prototipo o prueba de balance produzca una decisión de diseño nueva o cambie una existente.

| Campo | Valor |
| --- | --- |
| Estado | Preproducción y prototipado de reglas |
| Versión | 0.1 |
| Última revisión | 2026-07-21 |
| Plataforma de prototipado | Blazor WebAssembly |
| Presentación final prevista | Unity |

## Cómo mantener este documento

- Una regla acordada se incorpora a su sección y se refleja en **Decisiones establecidas**.
- Una cifra o mecánica aún no validada se marca como **provisional**; no debe tratarse como regla definitiva en `Wyrd.Core`.
- Una propuesta descartada se conserva en el historial de revisiones indicando el motivo, en vez de desaparecer sin contexto.
- Cada cambio significativo incrementa la versión del documento y añade una entrada breve al historial.
- El código, las pruebas y este documento deben usar la misma terminología para los conceptos del dominio.

## Historial de revisiones

| Versión | Fecha | Cambio |
| --- | --- | --- |
| 0.1 | 2026-07-21 | Primera consolidación del concepto, sistemas previstos, decisiones actuales y cuestiones abiertas. |

## 1. Concepto central

Project Wyrd es un juego de estrategia offline inspirado principalmente en *Empire Strike*, reinterpretado como una experiencia para un solo jugador ambientada en una fantasía de inspiración nórdica.

La partida enfrenta al jugador contra 99 imperios controlados por IA. No existen servidores, jugadores reales ni presión de tiempo: el mundo avanza únicamente cuando el jugador decide terminar el día.

La intención es conservar los aspectos más interesantes de *Empire Strike*:

- Construcción y administración de ciudades.
- Crecimiento de población.
- Reclutamiento de tropas.
- Héroes que lideran ejércitos.
- Conquista territorial.
- Clanes, guerras y control regional.
- Rankings y objetivos competitivos.

Todo ello se adapta a una campaña autocontenida y completamente simulada.

### Definición condensada

> Project Wyrd es un juego de estrategia offline inspirado en Empire Strike, donde el jugador desarrolla un imperio en un mundo de fantasía nórdica simulado junto a 99 imperios de IA. La campaña avanza por días controlados por el jugador e incluye ciudades, crecimiento de población, tropas, héroes, combates por rondas, clanes, guerras, control regional, magia global y rankings. Su lógica se implementa como un núcleo independiente en C#, reutilizable desde una aplicación web de simulación y una presentación final desarrollada en Unity.

## 2. Estructura de la campaña

Cada campaña tiene una duración limitada de días de juego. La duración exacta permanece abierta.

El jugador realiza sus acciones sin límite de tiempo real y después pulsa **Terminar día**. Al hacerlo:

- Crece la población.
- Se generan recursos.
- Avanzan construcciones y entrenamientos.
- Actúan los héroes y ejércitos de la IA.
- Se resuelven movimientos, ataques y eventos.
- Se recalculan los rankings y el control territorial.

El objetivo no tiene que ser eliminar a todos los rivales. Al concluir la campaña se compara el desempeño de cada imperio mediante distintos rankings.

## 3. Imperios controlados por IA

El mundo contiene 100 imperios:

- 1 imperio del jugador.
- 99 imperios controlados por IA.

Cada IA opera bajo las mismas reglas generales que el jugador y puede:

- Fundar o conquistar ciudades.
- Entrenar tropas.
- Mejorar asentamientos.
- Reclutar héroes.
- Formar ejércitos.
- Atacar o defender territorios.
- Unirse a clanes.
- Participar en guerras.
- Competir en los rankings.

Las IA no deben comportarse de manera idéntica. Sus personalidades o prioridades estratégicas podrían ser expansionistas, defensivas, económicas, religiosas o agresivas. La lista y el modelo de comportamiento concretos siguen abiertos.

## 4. Ciudades, población y producción

Las ciudades son el núcleo del imperio. Cada ciudad puede contener:

- Población.
- Producción de recursos.
- Edificios.
- Defensas.
- Tropas estacionadas.
- Un héroe asignado.
- Posición geográfica y tipo de terreno.

La población crece diariamente, con rendimientos decrecientes en niveles altos. Como referencia provisional, una ciudad avanzada podría crecer alrededor de 500 habitantes diarios o menos.

La población sirve para:

- Desbloquear tropas superiores.
- Permitir construcciones.
- Sostener ejércitos.
- Contribuir al valor general del imperio.

La fórmula exacta de crecimiento todavía debe definirse y probarse.

## 5. Sistema de tropas

Se propone una progresión de 20 tipos o escalones de tropas. Como referencia provisional, se desbloquearía una nueva tropa aproximadamente cada 5.000 habitantes.

La curva estudiada comienza con niveles consecutivos, continúa con saltos de dos y termina con saltos mayores, alcanzando escalones aproximados de 30 y 45.

Estos números no tienen que representar únicamente un nivel. Pueden condensar:

- Poder base.
- Requisito de población.
- Coste.
- Rareza.
- Complejidad de entrenamiento.

Cada raza dispone de su propia línea de tropas, con estadísticas y especializaciones diferentes. La tabla definitiva de tropas sigue abierta.

## 6. Razas y ambientación

La propuesta inicial contempla seis razas principales. La lista definitiva todavía no se ha decidido.

Aunque la ambientación se inspira en la mitología nórdica, los Æsir y los Vanir no se usarían como razas jugables normales. Encajan mejor como entidades divinas, facciones superiores o elementos narrativos.

Las razas mortales pueden diferenciarse mediante:

- Líneas de tropas propias.
- Crecimiento de población y economía.
- Afinidades con terrenos.
- Bonificaciones de combate.
- Acceso a magia o héroes.

La inspiración nórdica funciona como base creativa, sin obligar a reproducir literalmente los nueve reinos mitológicos.

## 7. Héroes

Cada ciudad puede albergar un héroe. Las cuatro clases principales definidas son:

- `Warrior`.
- `Thief`.
- `Mage`.
- `Priest`.

Los héroes lideran ejércitos y afectan directamente a los combates. El héroe actúa como comandante y no necesariamente como una unidad individual que sustituya a sus tropas.

Sus estadísticas previstas incluyen:

- Ataque.
- Defensa.
- Daño.
- Velocidad.
- Moral.
- Vida.
- Energía.
- Heridas.

La energía se consume con habilidades y magia. Las heridas representan consecuencias más persistentes que la pérdida de vida durante una batalla.

### Progresión por victorias

De forma provisional, un héroe recibe un punto de atributo cada ocho victorias. Esta progresión hace visible la experiencia militar sin depender únicamente de puntos de experiencia convencionales.

Los héroes contribuyen a rankings como:

- Mayor número de victorias.
- Mejor héroe.
- Héroe más poderoso.
- Mayor trayectoria militar.

## 8. Magos, sacerdotes y magia global

Los magos y sacerdotes permiten utilizar magia global de clan. Como umbral provisional, los hechizos de mayor importancia requieren aproximadamente nivel 12.

Los hechizos pueden afectar a:

- Regiones.
- Ciudades.
- Ejércitos.
- Producción.
- Moral.
- Puntos de acción.
- Imperios enemigos completos.

Durante las guerras de clanes, ciertos hechizos podrían robar o eliminar puntos de acción. Esto convierte la capacidad operativa en un recurso que se puede acumular, atacar y proteger.

## 9. Puntos de acción

Cada día el imperio recibe puntos de acción. La referencia provisional es:

- 100 puntos de acción fijos por día.
- Una cantidad adicional relacionada con el valor del imperio u otras bonificaciones.

Los puntos de acción se gastan en actividades como:

- Entrenar tropas.
- Construir edificios.
- Mejorar ciudades.
- Mover ejércitos.
- Atacar.
- Entrenar héroes.
- Utilizar habilidades.
- Ejecutar acciones diplomáticas.

Los puntos de acción pueden acumularse para preparar una guerra, ofensiva u otra fase de la campaña. Como contraparte, algunos hechizos enemigos pueden robarlos, reducirlos, bloquear temporalmente su uso o exigir recursos para protegerlos.

## 10. Ataques y paso del tiempo

Los puntos de acción determinan cuánto puede hacer un imperio, pero no deberían permitir conquistar una gran parte del mapa en un único día. Se propone separar dos límites:

- **Puntos de acción:** capacidad general para actuar.
- **Ventanas o límites de ataque:** número de ataques posibles durante el día.

Una división sencilla tendría mañana, tarde y noche. Una alternativa más detallada incluiría amanecer, mañana, mediodía, tarde, atardecer, noche y medianoche.

La cantidad de franjas y la entidad a la que se aplica el límite —ejército, ciudad o imperio— siguen abiertas.

## 11. Combate

El combate se resuelve por rondas y participan:

- Ejército atacante.
- Ejército defensor.
- Héroes.
- Tropas.
- Moral.
- Terreno.
- Defensas de la ciudad.
- Bonificaciones de raza, clase o afinidad.

Los factores relevantes pueden incluir ataque, defensa, velocidad, daño, orden de actuación, moral, heridas, composición del ejército, ventajas de terreno y fortificaciones.

Los combates deben permitir planificación suficiente, resolverse con rapidez y producir un informe claro. La profundidad exacta del sistema aún debe determinarse mediante prototipos.

## 12. Terreno y regiones

El mapa se divide en regiones y ciudades. El terreno proporciona bonificaciones y penalizaciones en batalla y, posiblemente, también en la economía.

Ejemplos de identidad territorial:

- Bosques favorables para emboscadas o unidades ligeras.
- Montañas favorables para defensores.
- Llanuras adecuadas para caballería o grandes ejércitos.
- Zonas heladas que penalizan la movilidad.
- Regiones mágicas o sagradas con efectos especiales.

La posición de una ciudad debe importar, evitando que todos los asentamientos sean intercambiables.

## 13. Clanes

Los imperios pueden organizarse en clanes. Aunque el juego sea individual, los demás miembros son imperios controlados por IA.

Los clanes permiten:

- Declarar guerras.
- Coordinar ataques.
- Compartir objetivos.
- Utilizar magia global.
- Controlar regiones.
- Competir en rankings colectivos.

El jugador puede liderar un clan, unirse a uno existente o permanecer independiente.

## 14. Control regional

Una región está gobernada por el clan que posee la mayor cantidad de ciudades dentro de ella.

Controlar una región puede conceder:

- Bonificaciones económicas.
- Producción adicional.
- Prestigio o fama.
- Ventajas de movimiento.
- Bonos de combate.
- Acceso a edificios o tropas especiales.
- Puntos de ranking.

La conquista de una sola ciudad puede cambiar el control de toda una región, creando objetivos territoriales concretos para las guerras.

## 15. Guerras de clanes

Las guerras de clanes no son únicamente una sucesión de ataques individuales. Incorporan:

- Objetivos territoriales.
- Ataques coordinados.
- Magia global.
- Desgaste de puntos de acción.
- Control regional.
- Recompensas colectivas.
- Rankings de guerra.

Los magos y sacerdotes de nivel alto tienen especial importancia durante estas guerras, incluida la posibilidad de atacar la capacidad operativa enemiga.

## 16. Fama y monumentos

La fama es un recurso producido principalmente por monumentos y puede gastarse en beneficios especiales. Representa prestigio cultural, legitimidad, influencia religiosa, reconocimiento militar y poder simbólico.

Sus usos potenciales incluyen:

- Bonificaciones permanentes o temporales.
- Acceso a héroes.
- Eventos especiales.
- Mejoras de clan.
- Magia o bendiciones.
- Desempates en rankings.

Los efectos y costes concretos siguen abiertos.

## 17. Rankings y condiciones de victoria

La campaña ofrece varios rankings para permitir distintas formas de destacar:

- Valor del imperio o `Empire Value`.
- Mayor número de ciudades.
- Mejor economía.
- Mayor población.
- Mayor índice bélico.
- Héroe con más victorias.
- Imperio con mejores héroes.
- Mayor cantidad de territorios controlados.
- Mayor fama.
- Mejor clan.

La clasificación principal probablemente será `Empire Value`, pero el jugador puede perseguir objetivos secundarios o victorias especializadas. La fórmula de valor del imperio aún no está definida.

## 18. Sistema de ventajas zodiacales

Se propone un sistema de afinidades basado en signos zodiacales, elementos y polaridades. Encaja con el concepto de destino de Wyrd y puede aportar una capa adicional a las relaciones entre unidades.

Cada unidad, héroe o ejército podría tener:

- Un signo.
- Un elemento: fuego, tierra, aire o agua.
- Una polaridad.

Estas propiedades generarían relaciones favorables o desfavorables. Para evitar una complejidad excesiva, deberían expresarse mediante efectos sencillos, como:

- Un modificador pequeño de combate.
- Sinergia entre héroe y tropas.
- Afinidad con determinadas regiones.
- Calendarios o estaciones que favorezcan algunos signos.

La tabla completa de relaciones y el alcance definitivo del sistema siguen abiertos.

## 19. Identidad visual

Wyrd debe distinguirse visualmente de Scoundrel:

- **Scoundrel:** low poly muy claro, cercano a Synty.
- **Project Wyrd:** estilo toon o estilizado más rico, con mayor variedad ambiental.

Wyrd necesita representar reinos, regiones, terrenos de batalla, ciudades, encuentros y cambios climáticos o geográficos. Los biomas estilizados son una referencia adecuada, aunque el proyecto no se plantea como un MMO.

## 20. Arquitectura técnica

La lógica del juego se separa completamente de su presentación.

### Núcleo en C#

`Wyrd.Core` debe contener:

- Reglas de ciudades.
- Población.
- Recursos.
- Tropas.
- Héroes.
- Combate.
- Clanes.
- IA.
- Paso de días.
- Rankings.
- Guardado de partida.
- Simulación del mundo.

El núcleo es una biblioteca C# sin dependencias directas de Unity ni Blazor.

### Presentaciones intercambiables

Sobre el núcleo pueden construirse:

- Una aplicación web para prototipar y ajustar reglas rápidamente.
- Herramientas de simulación.
- Una interfaz de administración y depuración.
- La versión final en Unity.

La aplicación web permite ejecutar días rápidamente, observar decisiones de la IA y ajustar el balance sin levantar continuamente una escena compleja de Unity.

Unity se ocupa principalmente de visualización, animaciones, audio, interfaz, mapas, efectos y presentación de combates.

## 21. LOVE/HATE y comportamiento de IA

LOVE/HATE podría utilizarse en Unity para representar amistad, rivalidad, odio, lealtad, miedo, respeto y confianza entre imperios.

Para evitar acoplamiento con Unity, las relaciones básicas deben existir en `Wyrd.Core` mediante valores y reglas propias. LOVE/HATE puede actuar después como una capa adicional de comportamiento o presentación, pero no será un requisito del simulador.

## 22. Estado actual del diseño

### Decisiones establecidas

- Juego completamente offline y para un solo jugador.
- 100 imperios en total: el jugador y 99 IA.
- Avance manual mediante días.
- Campaña con duración limitada.
- Ciudades, población, tropas y héroes como pilares.
- Cuatro clases principales de héroe.
- Combates resueltos por rondas.
- Puntos de acción acumulables.
- Clanes, guerras y control regional.
- Control regional por mayoría de ciudades del clan.
- Terrenos con modificadores.
- Múltiples rankings.
- Núcleo independiente en C#.
- Blazor como entorno de prototipado y Unity como presentación final.
- Las relaciones fundamentales de IA deben pertenecer al núcleo, no depender de LOVE/HATE.

### Valores y propuestas provisionales

- 20 tipos o escalones de tropas.
- Una tropa nueva aproximadamente cada 5.000 habitantes.
- Seis razas jugables iniciales.
- Un punto de atributo de héroe cada ocho victorias.
- Magia global importante a partir de nivel 12.
- 100 puntos de acción base por día, más bonificaciones.
- Fases diferenciadas dentro de cada día para limitar ataques.
- `Empire Value` como clasificación principal.
- Afinidades zodiacales como modificadores pequeños.

### Cuestiones abiertas

- Número exacto de días por campaña.
- Cantidad y función de los recursos.
- Lista final de razas.
- Tabla completa de las 20 tropas.
- Fórmula de crecimiento de población.
- Fórmula de `Empire Value`.
- Límites diarios de ataques.
- Profundidad exacta del combate.
- Personalidades y comportamiento de las IA.
- Sistema diplomático.
- Efectos concretos de fama.
- Reglas zodiacales.
- Nombres definitivos de regiones y mapas.
- Alcance de la primera versión jugable.

## 23. Siguiente decisión recomendada

Definir el alcance de la primera simulación vertical: una campaña corta con un número reducido de imperios, una única región, ciudades, producción diaria y ranking básico. Esta rebanada permitiría validar el ciclo **actuar → terminar día → simular mundo → revisar resultados** antes de ampliar tropas, combate, clanes y magia.
