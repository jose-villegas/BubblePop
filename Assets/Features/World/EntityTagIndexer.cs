using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class EntityTagIndexer : IComponent
{
    public Dictionary<string, IEntity> Value;
}