using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class BubbleVerticalOffsetComponent : IComponent
{
    public float Value;
}