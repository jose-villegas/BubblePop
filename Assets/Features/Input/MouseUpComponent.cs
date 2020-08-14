using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input, Event(EventTarget.Any)]
public sealed class MouseUpComponent : IComponent
{
    public Vector3 value;
    public int button;
}