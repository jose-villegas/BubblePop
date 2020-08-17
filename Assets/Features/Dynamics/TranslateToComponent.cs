using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using EventType = Entitas.CodeGeneration.Attributes.EventType;

[Game, Event(EventTarget.Self, EventType.Removed)]
public sealed class TranslateToComponent : IComponent
{
    public float Speed;
    public Vector3 Value;
}