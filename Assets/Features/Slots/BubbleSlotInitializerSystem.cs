using Entitas;
using UnityEngine;

/// <summary>
/// This system creates the initial bubble slots where
/// bubble with values can exists
/// </summary>
public class BubbleSlotInitializerSystem : IInitializeSystem
{
    private Contexts _contexts;

    public BubbleSlotInitializerSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var e = _contexts.game.CreateEntity();
                var horizontalIndex = j * 2 + i % 2;
                var verticalIndex = i - 2;
                e.ReplaceBubbleSlot(new Vector2Int(horizontalIndex, verticalIndex));
                e.ReplacePosition(new Vector3(e.bubbleSlot.Value.x, e.bubbleSlot.Value.y));
                e.ReplaceAsset("Bubble");
            }
        }
    }
}