using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// When a bubble stops after colliding with another
/// this components stores the entity that the stopped
/// bubble collided with
/// </summary>
[Game, Unique]
public sealed class CollidedWithBubblesComponent : IComponent
{
    public GameEntity[] Value;
}