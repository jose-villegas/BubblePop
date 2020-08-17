﻿using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, Event(EventTarget.Self)]
public sealed class ScoreComponent : IComponent
{
    public int Value;
}