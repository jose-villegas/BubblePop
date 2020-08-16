using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Component indicating if an entity is a bubble
/// </summary>
[Game, Event(EventTarget.Self)]
public sealed class BubblePlayFXComponent : IComponent
{
}