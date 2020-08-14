using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class Trigger2DEventComponent : IComponent
{
    public Collider2D Value;
}