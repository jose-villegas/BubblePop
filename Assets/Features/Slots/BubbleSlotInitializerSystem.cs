using Entitas;
using UnityEngine;

/// <summary>
/// This system creates the initial bubble slots where
/// bubble with values can exists
/// </summary>
public class BubbleSlotInitializerSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public BubbleSlotInitializerSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var iterator = new BubbleSlotIterator(6, 8);

        foreach (Vector2Int index in iterator)
        {
            var e = _contexts.game.CreateEntity();
            e.ReplaceBubbleSlot(index);
        }
    }
}