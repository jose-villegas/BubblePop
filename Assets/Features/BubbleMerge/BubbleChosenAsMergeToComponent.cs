using Entitas;
using Entitas.CodeGeneration.Attributes;

/// <summary>
/// The entity that will serve as final point for a merge
/// stores the final number of the merge process
/// </summary>
[Game, Unique]
public sealed class BubbleChosenAsMergeToComponent : IComponent
{
    public int Value;
}