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
            entities[0].Destroy();

            // mark all bubbles as disconnected bubbles
            foreach (var entity in _group.AsEnumerable().ToList())
            {
                entity.isBubbleConnected = false;
                entity.isBubbleConnectionCheck = false;
            }

            var limit = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
            var iterator = new BubbleSlotIterator(6, limit + 1, limit);

            foreach (Vector2Int slotIndex in iterator)
            {
                if (_contexts.game.bubbleSlotIndexer.Value.TryGetValue(slotIndex, out var entity))
                {
                    var gameEntity = (GameEntity) entity;

                    // since this is the top line, it's connected, we need to check for its neighbors
                    gameEntity.isBubbleConnected = true;

                    var neighbors = _contexts.game.GetBubbleNeighbors(gameEntity.bubbleSlot);

                    foreach (var neighbor in neighbors)
                    {
                        // only consider neighbors below
                        if (neighbor.bubbleSlot.Value.y >= gameEntity.bubbleSlot.Value.y) continue;

                        // the neighbor is connected as they are connected to the top line
                        neighbor.isBubbleConnected = true;
                        // still need to connect with the neighbor's neighbors
                        neighbor.isBubbleConnectionCheck = true;
                    }
                }
            }

            return;
        }

        bool allConnectionsChecked = true;

        foreach (var gameEntity in entities)
        {
            var neighbors = _contexts.game.GetBubbleNeighbors(gameEntity.bubbleSlot);

            // first we check if we are connected
            foreach (var neighbor in neighbors)
            {
                if (!neighbor.isBubbleConnected)
                {
                    // since we are connected it means that our neighbors are also connected
                    neighbor.isBubbleConnected = true;
                    // still need to connect with the neighbor's neighbors
                    neighbor.isBubbleConnectionCheck = true;
                    // we haven't reached the point where all neighbors are connected
                    allConnectionsChecked = false;
                }
            }
        }

        if (allConnectionsChecked)
        {
            // mark disconnected bubbles
            foreach (var entity in _group.AsEnumerable().ToList())
            {
                if (!entity.isBubbleConnected)
                {
                    _contexts.game.RemoveSlotIndex(entity);

                    entity.AddBubbleFalling(Vector3.zero);
                    entity.isStableBubble = false;
                    entity.ReplaceLayer(LayerMask.NameToLayer("FallingBubbles"));
                }
            }
        }
    }
}