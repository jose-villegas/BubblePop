using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Component to indicate that the destruction FX should be played
/// </summary>
[Game, Event(EventTarget.Self)]
public sealed class BubblePlayDestroyFXComponent : IComponent
{
}