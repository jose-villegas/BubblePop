using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This system creates the initial and assigns their slot
/// </summary>
public class GameStartBubblesSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;

    public GameStartBubblesSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    public GameStartBubblesSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public GameStartBubblesSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameStarted);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isGameEvent && entity.isGameStarted;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var configuration = _contexts.configuration.gameConfiguration.value;
        var indexer = _contexts.game.bubbleSlotIndexer;

        var iterator = new BubbleSlotIterator(6, configuration.InitialRowCount);

        foreach (Vector2Int index in iterator)
        {
            // create initial bubbles
            var e = _contexts.game.CreateEntity();
            e.isBubble = true;
            e.isStableBubble = true;

            e.AddPosition(Vector3.zero);
            e.AddAsset("Bubble");
            // establish link with slot space
            var slotEntity = indexer.Value[index];
            e.AddBubbleSlotLink(slotEntity);
        }
    }
}