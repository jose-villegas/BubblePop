using System.Collections.Generic;
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
        var configuration = _contexts.configuration.gameConfiguration.value;
        var iterator = new BubbleSlotIterator(6, configuration.InitialRowCount);

        // create indexer
        var indexerEntity = _contexts.game.CreateEntity();
        indexerEntity.AddBubbleSlotIndexer(new Dictionary<Vector2Int, IEntity>());

        foreach (Vector2Int index in iterator)
        {
            // index the slot component for faster lookup
            var modifiedIndex = new Vector2Int(index.x, index.y + configuration.SlotsInitialVerticalIndex);

            var e = _contexts.game.CreateEntity();
            e.ReplaceBubbleSlot(modifiedIndex);
            indexerEntity.bubbleSlotIndexer.Value[modifiedIndex] = e;
        }
    }
}