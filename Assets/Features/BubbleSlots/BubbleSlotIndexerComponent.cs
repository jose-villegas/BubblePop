using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

/// <summary>
/// This single component indexes all bubble slot
/// entity instances for faster lookup
/// </summary>
[Game, Unique]
public sealed class BubbleSlotIndexerComponent : IComponent
{
    public Dictionary<Vector2Int, IEntity> Value;
}