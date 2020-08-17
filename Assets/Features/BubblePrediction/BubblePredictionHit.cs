using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, Event(EventTarget.Self)]
public sealed class BubblePredictionHit : IComponent
{
    public GameEntity Value;
    public Vector3 Point;
}