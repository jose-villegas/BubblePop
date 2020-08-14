using System;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Configuration, Unique, ComponentName("GameConfiguration")]
public interface IGameConfiguration
{
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
    /// Determines the initial vertical index of the first instanced
    /// slots, instead of starting from zero
    /// </summary>
    int SlotsInitialVerticalIndex { get; }

    Vector3 BubbleScale { get; }

    Vector3 NextBubbleScale { get; }

    float OverlapCircleRadius { get; }
    float MergeTranslateSpeed { get; }
    float ReloadSpeed { get; }
}