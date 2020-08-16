using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Component indicating that the explosion FX should be played
/// </summary>
[Game, Event(EventTarget.Self)]
public sealed class BubblePlayExplosionFXComponent : IComponent
{
}