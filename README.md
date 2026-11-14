# Knight Platform — Actividad Práctica 4: Enemigos

Videojuego de plataformas 2D desarrollado con **Unity 6 y C#** para la asignatura **Programación de Videojuegos (UFHEC)**.

## Contenido de la práctica

En **Level01** se incorporaron enemigos con sprites y animaciones:

- **Enemigo simple:** patrulla una distancia configurada mediante `SimpleEnemy.cs`.
- **Enemigo complejo:** patrulla, se detiene y persigue al jugador cuando lo detecta, mediante `ComplexEnemy.cs`.
- **Colisiones:** el contacto físico con un enemigo reinicia el nivel mediante `EnemyCollision.cs`.
- **Configuración:** enemigos identificados con la etiqueta `Enemy` y la capa `Enemies`, con componentes de física y colisiones 2D.

## Ampliación en desarrollo

Se creó **Level02**, un escenario medieval para el futuro combate final. Incluye un dragón con animaciones de reposo, vuelo y ataque de fuego, controlado mediante `DragonBoss.cs`. Esta escena aún está en desarrollo y no es parte de los requisitos obligatorios de esta práctica.

## Tecnologías

- Unity 6
- C#
- Física 2D y Animator

## Evidencia

[Video de funcionamiento del juego](https://canva.link/5ry919nqaibkns7)

## Próximos pasos

Completar la transición entre escenas y las mecánicas del enfrentamiento final en la siguiente práctica.
