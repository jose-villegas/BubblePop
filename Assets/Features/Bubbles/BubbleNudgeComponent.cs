using Entitas;
using UnityEngine;

/// <summary>
/// Component indicating if a bubble entity should
/// execute a small nudge animation
/// </summary>
[Game]
public sealed class BubbleNudgeComponent : IComponent
{
    public Vector3 Direction;
    public Vector3 Origin;
    public bool Return;
}