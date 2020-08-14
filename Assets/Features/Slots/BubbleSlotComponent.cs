using Entitas;
using UnityEngine;

/// <summary>
/// This represents the index value for a bubble
/// slot within the bubble slots structure
/// </summary>
[Game]
public sealed class BubbleSlotComponent : IComponent
{
    public Vector2Int Value;
}