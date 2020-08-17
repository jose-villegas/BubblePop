using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Contains the merge number
/// </summary>
[Game, Unique]
public sealed class BubblesReadyToMergeComponent : IComponent
{
    public int Value;
}