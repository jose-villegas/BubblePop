using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Any)]
public sealed class PlayAudioComponent : IComponent
{
    public string Value;
}