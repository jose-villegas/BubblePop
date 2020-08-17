using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, Event(EventTarget.Self)]
public sealed class BubbleTraceComponent : IComponent
{
    public List<Vector3> Values;
}