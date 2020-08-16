using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

/// <summary>
/// This represents the index value for a bubble
/// slot within the bubble slots structure
/// </summary>
[Game, Event(EventTarget.Any)]
public sealed class BubbleSlotComponent : IComponent
{
    public Vector2Int Value;
}