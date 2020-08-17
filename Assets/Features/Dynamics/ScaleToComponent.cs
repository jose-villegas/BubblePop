using Entitas;
using UnityEngine;

[Game]
public sealed class ScaleToComponent : IComponent
{
    public float Speed;
    public Vector3 Value;
}