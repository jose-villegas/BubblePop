using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Represents the value of a filled bubble slot
/// </summary>
[Game, Event(EventTarget.Self)]
public sealed class BubbleNumberComponent : IComponent
{
    public int Value;
}