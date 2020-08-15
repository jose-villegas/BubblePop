using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This systems determines from top to bottom which bubbles are disconnected
/// from the top most line
/// </summary>
public class BubbleDisconnectionCheckSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _group;

    public BubbleDisconnectionCheckSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _group = contexts.game.GetGroup(GameMatcher.StableBubble);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleConnectionCheck);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubbleConnectionCheck;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // in this case we have received a connection check as trigger event
        // add top most bubbles to be checked
        if (entities.Count == 1 && !entities[0].isBubble)
        {
            var limit = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
            var iterator = new BubbleSlotIterator(6, limit + 1, limit);

            foreach (Vector2Int slotIndex in iterator)
            {
                var entity = _contexts.game.bubbleSlotIndexer.Value[slotIndex] as GameEntity;
                // since this is the top line, it's connected, we need to check for its neighbors
                entity.isBubbleConnected = true;

                var neighbors = _contexts.game.GetBubbleNeighbors(entity.bubbleSlot);

                foreach (var neighbor in neighbors)
                {
                    if (!neighbor.isBubbleConnected)
                    {
                        neighbor.isBubbleConnectionCheck = true;
                    }
                }
            }
        }
        else
        {
            bool bottomReached = true;

            foreach (var gameEntity in entities)
            {
                var neighbors = _contexts.game.GetBubbleNeighbors(gameEntity.bubbleSlot);

                foreach (var neighbor in neighbors)
                {
                    if (neighbor.isBubbleConnected)
                    {
                        gameEntity.isBubbleConnected = true;
                        gameEntity.isBubbleConnectionCheck = false;
                        continue;
                    }

                    // when all the neighbors are connected we can assume we have reached
                    // the bottom of the bubble structure
                    bottomReached = false;

                    neighbor.isBubbleConnectionCheck = true;
                }
            }

            if (bottomReached)
            {
                // mark disconnected bubbles
                foreach (var entity in _group.AsEnumerable().ToList())
                {
                    if (!entity.isBubbleConnected)
                    {
                        entity.isBubbleDisconnected = true;
                        entity.isStableBubble = false;
                        entity.RemoveBubbleSlot();
                        entity.ReplaceLayer(LayerMask.NameToLayer("FallingBubbles"));
                        entity.isDestroyed = true;
                    }

                    // remove connection flags, for next instance
                    entity.isBubbleConnected = false;
                    entity.isBubbleConnectionCheck = false;
                }
            }
        }
    }
}