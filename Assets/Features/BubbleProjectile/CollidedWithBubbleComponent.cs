using Entitas;

/// <summary>
/// When a bubble stops after colliding with another
/// this components stores the entity that the stopped
/// bubble collided with
/// </summary>
[Game]
public class CollidedWithBubbleComponent : IComponent
{
    public GameEntity Value;
}