using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// After a bubble is stopped and marked as unstable this system
/// links it to a new slot, yet unmerged
/// </summary>
public class BubbleSlotterSystem : ReactiveSystem<GameEntity>, ITranslateToRemovedListener
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;

    public BubbleSlotterSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CollidedWithBubbles));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.hasCollidedWithBubbles;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            var colliders = gameEntity.collidedWithBubbles.Value;

            foreach (var collider in colliders)
            {
                var pos = gameEntity.position.Value;
                var colliderPos = collider.position.Value;
                var direction = (pos - colliderPos).normalized;

                // collision angle, used to determine where the bubble will be when slotted
                var angle = Mathf.Atan2(direction.y, direction.x);

                // obtain new slot index from the collider slot position
                var colliderSlot = collider.bubbleSlot;
                var newSlotIndex = colliderSlot.CalculateSlotIndexAtAngle(angle);

                // check if this slot is already occupied we may be taking the wrong collider
                var indexer = _contexts.game.bubbleSlotIndexer.Value;

                if (indexer.ContainsKey(newSlotIndex))
                {
                    continue;
                }

                var finalPosition = newSlotIndex.IndexToPosition(_contexts.game, _configuration);

                // new slot to store this bubble
                gameEntity.ReplaceBubbleSlot(newSlotIndex);

                // move to final position
                gameEntity.AddTranslateTo(_configuration.ProjectileSpeed, finalPosition);
                gameEntity.AddTranslateToRemovedListener(this);
                gameEntity.isMoving = true;

                // find which neighbors to nudge
                var neighbors = _contexts.game.GetBubbleNeighbors(gameEntity.bubbleSlot);

                foreach (var neighbor in neighbors)
                {
                    if (neighbor.bubbleNumber.Value != gameEntity.bubbleNumber.Value)
                    {
                        var nudgeDir = (neighbor.position.Value - finalPosition).normalized;
                        neighbor.ReplaceBubbleNudge(nudgeDir, neighbor.position.Value, false);
                    }
                }

                // we only need to find one valid slot to finish
                break;
            }
        }
    }

    public void OnTranslateToRemoved(GameEntity entity)
    {
        Debug.Log("Set Waiting for Merge");
        entity.isMoving = false;
        // let the merge system now take charge
        entity.isBubbleWaitingMerge = true;
        entity.isUnstableBubble = false;

        entity.RemoveTranslateToRemovedListener(this);
    }
}