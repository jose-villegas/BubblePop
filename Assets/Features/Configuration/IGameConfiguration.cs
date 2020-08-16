using System;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Configuration, Unique, ComponentName("GameConfiguration")]
public interface IGameConfiguration
{
    /// <summary>
    /// This contains specific setup per exponent
    /// </summary>
    List<ExponentConfiguration> ExponentConfigurations { get; }

    /// <summary>
    /// Determines the maximum possible value for a bubble
    /// </summary>
    int MaximumExponent { get; }

    /// <summary>
    /// When bubbles are spawned, this determined their
    /// maximum assigned value, unlike <see cref="MaximumExponent"/>
    /// that can be reached through merging
    /// </summary>
    int MaximumSpawnExponent { get; }

    /// <summary>
    /// This defines how many rows will be filled with
    /// starting bubbles at game start
    /// </summary>
    int InitialRowCount { get; }

    Vector2 SpawnScaleSpeedRange { get; }

    /// <summary>
    /// This determines the separation scale between
    /// bubbles set in slots, the instancing slot position
    /// is used to determine the world position, this property
    /// modifies the strength of this assignation
    /// </summary>
    /// <example>
    /// Given a slot index of (i, j) and a separation of (x, y)
    /// the world position is determined as:
    ///
    /// Horizontally : (i - 5) * x - x * 0.5
    /// Vertically   : j * y
    /// </example>
    Vector2 BubblesSeparation { get; }

    /// <summary>
    /// This determines the height at where the projectile bubbles
    /// will be spawned
    /// </summary>
    float ProjectileBubblesHeight { get; }
   
    float ProjectileSpeed { get; }

    /// <summary>
    /// Determines the initial vertical height of the first instanced
    /// slots
    /// </summary>
    float SlotPositioningVerticalHeight { get; }

    Vector3 BubbleScale { get; }

    /// <summary>
    /// The scale for the next loaded bubble projectile.
    /// </summary>
    Vector3 NextBubbleScale { get; }

    /// <summary>
    /// This circle is used to detect collision for the bouncing system
    /// </summary>
    float OverlapCircleRadius { get; }

    /// <summary>
    /// Translation speed for bubbles to merge
    /// </summary>
    float MergeTranslateSpeed { get; }

    /// <summary>
    /// The translation and scale animation speed on reload
    /// </summary>
    float ReloadSpeed { get; }

    /// <summary>
    /// The values determine when to scroll up or down
    /// x is used to determine when to scroll down
    /// and y is used to determine when to scroll up
    ///
    /// If any bubble is past the y height value, the
    /// scrolling system will go up
    ///
    /// If all the bubbles are on top the x height value
    /// the scrolling system will go down
    /// </summary>
    Vector2 ScrollingBubblePositionBounds { get; }

    /// <summary>
    /// This determines the height of the top-most
    /// line, if lines are under this value, new are created
    /// </summary>
    float LinesHeight { get; }

    /// <summary>
    /// Scrolling animation speed
    /// </summary>
    float ScrollingSpeed { get; }

    /// <summary>
    /// The nudge animation happens when a bubble projectile
    /// is thrown and sucesfully slotted, triggering a "nudge"
    /// push around all its neighbors, this controll how strong
    /// the nudge push is
    /// </summary>
    float NudgeDistance { get; }

    float NudgeSpeed { get; }

    /// <summary>
    /// This circle is used to detect collision for the tracing system
    /// </summary>
    float CircleCastRadius { get; }

    /// <summary>
    /// When a bubble is set as falling, it's destroyed when it gets
    /// past this height value
    /// </summary>
    float FallingDeadZoneHeight { get; }

    float FallingGravity { get; }
}