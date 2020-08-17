using System.Numerics;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class BubbleSlotLimitsIndexComponent : IComponent
{
    public int MaximumVertical;
    public int MinimumVertical;
}