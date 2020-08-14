using System;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Configuration, Unique, ComponentName("GameConfiguration")]
public interface IGameConfiguration
{
    List<int> PossibleBubbleValues { get; }

    /// <summary>
    /// This defines how many rows will be filled with
    /// starting bubbles at game start
    /// </summary>
    int InitialRowCount { get; }

    Vector2 BubblesSeparation { get; }

    float ProjectileBubblesHeight { get; }
}