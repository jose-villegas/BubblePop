using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Indicates when a new bubble has been successfully inserted
/// </summary>
[Unique, Game, Event(EventTarget.Self)]
public sealed class BubbleInsertedComponent :  IComponent
{
}