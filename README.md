# Actividad Práctica 3: Objetos 2D — Knight Platform

## Descripción

Ampliación del videojuego **Knight Platform**, desarrollado en **Unity 6 y C#**, mediante la implementación de un sistema de recolección de objetos en un escenario de plataformas 2D con temática medieval.

## Funcionalidades implementadas

- Cinco alimentos coleccionables: **Cake, Chicken, Coffee, Jam y Cookie**.
- Detección mediante `OnTriggerEnter2D` y colliders con **Is Trigger** activado.
- Identificación de objetos con el **Tag `Collectible`** y la **Layer `Collectibles`**.
- Desaparición de cada objeto después de recogerlo.
- Contadores independientes actualizados en pantalla mediante **TextMeshPro**.
- Interfaz con fondo semitransparente para mejorar la lectura.
- Conservación del movimiento, salto, animaciones y meta del nivel.

## Tecnologías utilizadas

- **Unity 6**
- **C#**
- **TextMeshPro**
- **Rigidbody2D** y **BoxCollider2D**
- **Git y GitHub**

## Controles y funcionamiento

| Acción | Tecla |
|---|---|
| Moverse a la izquierda o derecha | `A` / `D` o flechas `←` / `→` |
| Saltar | `Espacio` |

El jugador controla a un caballero que explora las plataformas y recoge alimentos. Cuando entra en contacto con uno, el objeto desaparece y su contador aumenta automáticamente. Al llegar a la meta, puede completar y reiniciar el nivel.

## Repositorio

- [Knight Platform en GitHub](https://github.com/juniorvaleraofficial/ActividadPracticaUnidad2_KnightPlatform)
- **Rama de esta práctica:** [`3-actividad-practica-2`](https://github.com/juniorvaleraofficial/ActividadPracticaUnidad2_KnightPlatform/tree/3-actividad-practica-2)

## Autor

**Junior Alexis Valera Rijo**  
**Matrícula:** MT-2023-00518  
**Universidad:** UFHEC  
**Asignatura:** INF-4317 — Programación de Videojuegos
