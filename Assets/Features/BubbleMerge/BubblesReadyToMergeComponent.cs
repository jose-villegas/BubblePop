using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// Contains the merge number
/// </summary>
[Game, Unique]
public class BubblesReadyToMergeComponent : IComponent
{
    public int Value;
}