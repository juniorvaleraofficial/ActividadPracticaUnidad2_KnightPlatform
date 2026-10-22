# Knight Platform — Actividad Práctica: Animaciones 2D

Videojuego de plataformas 2D desarrollado en **Unity 6** con **C#** para la asignatura **INF-4317 — Programación de Videojuegos** de la **Universidad UFHEC**.

Este trabajo continúa el proyecto de la unidad anterior y adapta las animaciones del caballero a los requisitos de la actividad **«Animaciones 2D»**.

## Objetivo de la práctica

Configurar sprites, colisiones y animaciones 2D para que el personaje cambie automáticamente entre reposo (**Idle**), desplazamiento (**Run**) y salto (**Jump**) según las acciones del jugador.

## Descripción del juego

El jugador controla a un caballero en un escenario medieval con plataformas de piedra. Puede desplazarse en ambas direcciones, saltar, superar las plataformas y alcanzar una bandera que completa el nivel. Al llegar a la meta, aparece una pantalla de victoria con la opción **«Volver a jugar»**.

## Controles

| Acción | Tecla |
| --- | --- |
| Mover a la izquierda | `A` o `←` |
| Mover a la derecha | `D` o `→` |
| Saltar | `Espacio` |
| Reiniciar al completar el nivel | Botón «Volver a jugar» |

## Animaciones del personaje

Los sprites están divididos en fotogramas de **96 × 84 píxeles** y se utilizan en tres clips:

| Clip | Acción | Fotogramas | Sample Rate |
| --- | --- | ---: | ---: |
| `Player_Idle` | Caballero en reposo | 7 | 12 FPS |
| `Player_Run` | Caballero caminando o corriendo | 8 | 12 FPS |
| `Player_Jump` | Caballero saltando | 5 | 12 FPS |

`Player_Idle` es el estado predeterminado del **Animator**. Los clips se seleccionan por condiciones en sus transiciones, con **Has Exit Time desactivado** y **Transition Duration = 0**.

### Parámetros del Animator

- **`Speed` (float):** representa el movimiento horizontal. Cuando supera `0.1`, activa `Player_Run`; al quedar por debajo de `0.1`, vuelve a `Player_Idle` si no está saltando.
- **`IsJumping` (bool):** cuando es `true`, activa `Player_Jump` desde `Any State`; al aterrizar cambia a `false`, permitiendo volver a reposo o movimiento según `Speed`.

Las transiciones de salida del salto incluyen ambas condiciones (`IsJumping` y `Speed`) para determinar si el personaje aterriza quieto o desplazándose.

### Control de animaciones desde C#

El script [`Assets/Scripts/PlayerMovement.cs`](Assets/Scripts/PlayerMovement.cs) gestiona el movimiento y actualiza los parámetros del Animator:

```csharp
animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
animator.SetBool("IsJumping", true);  // Al iniciar el salto
animator.SetBool("IsJumping", false); // Al aterrizar sobre Ground
```

## Componentes de Unity

El objeto **Player** incorpora:

- **Sprite Renderer:** muestra el sprite inicial `IDLE_0` y los fotogramas animados.
- **BoxCollider2D:** representa el área de colisión del cuerpo del personaje.
- **Rigidbody2D:** aplica gravedad y permite el desplazamiento físico.
- **Animator:** administra los estados y las transiciones entre animaciones.
- **PlayerMovement:** controla los desplazamientos, el salto y los parámetros de animación.

El script [`Assets/Scripts/Goal.cs`](Assets/Scripts/Goal.cs) detecta cuándo el jugador alcanza la meta, muestra el panel de nivel completado y permite reiniciar la escena.

## Estructura principal

```text
Assets/
├── Animations/
│   ├── Player.controller
│   ├── Player_Idle.anim
│   ├── Player_Run.anim
│   └── Player_Jump.anim
├── Art/
│   ├── Characters/
│   ├── Background/
│   └── Environment/
├── Scenes/
│   └── MainLevel.unity
└── Scripts/
    ├── PlayerMovement.cs
    └── Goal.cs
```

## Cómo ejecutar el proyecto

1. Clonar o descargar el repositorio.
2. Abrir la carpeta del proyecto desde **Unity Hub**, con una versión compatible de Unity 6 (el proyecto se editó con **6000.6.2f1**).
3. Abrir la escena `Assets/Scenes/MainLevel.unity`.
4. Presionar **Play** en Unity para iniciar el juego.

## Entrega académica

La entrega de **Animaciones 2D** incluye este proyecto de Unity y un **PDF de evidencias** con capturas de los sprites, el Sprite Renderer, el BoxCollider2D, los clips de animación, el Animator, el código C# y el juego en ejecución.

**Rama de trabajo de esta entrega:** [`practica-3-presentacion`](https://github.com/juniorvaleraofficial/ActividadPracticaUnidad2_KnightPlatform/tree/practica-3-presentacion)

## Autor

**Junior Alexis Valera Rijo**  
**Matrícula:** MT-2023-00518  
**Universidad:** UFHEC  
**Asignatura:** INF-4317 — Programación de Videojuegos

**Repositorio:** https://github.com/juniorvaleraofficial/ActividadPracticaUnidad2_KnightPlatform
