using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class BubbleVerticalOffsetComponent : IComponent
{
    public float Value;
}