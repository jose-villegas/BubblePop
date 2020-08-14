using System;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

[Configuration, Unique, ComponentName("GameConfiguration")]
public interface IGameConfiguration
{
    List<int> PossibleBubbleValues { get; }
}