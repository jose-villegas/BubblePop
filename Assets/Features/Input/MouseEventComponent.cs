using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input, Event(EventTarget.Any)]
public class MouseEventComponent : IComponent
{
    public Vector3 value;
    public int button;
}