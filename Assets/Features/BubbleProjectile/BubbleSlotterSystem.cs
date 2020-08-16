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
            var indexed = false;

            foreach (var collider in colliders)
            {
                var pos = gameEntity.position.Value;
                var colliderPos = collider.position.Value;
                var direction = (pos - colliderPos).normalized;

                // collision angle, used to determine where the bubble will be when slotted
                var angle = Mathf.Atan2(direction.y, direction.x);

                indexed = IndexWithCurrentAngle(gameEntity, collider.bubbleSlot, angle);

                if (indexed) break;
            }

            if (!indexed)
            {
                // we couldn't find an empty slot with the given direction
                // this is a hack given time constrains
                // todo: improve slotting detection
                FindClosestAvailableNeighborSlot(colliders, gameEntity);
            }
        }
    }

    private void FindClosestAvailableNeighborSlot(GameEntity[] colliders, GameEntity gameEntity)
    {
        foreach (var collider in colliders)
        {
            var pos = gameEntity.position.Value;
            var colliderPos = collider.position.Value;
            var direction = (pos - colliderPos).normalized;

            // collision angle, used to determine where the bubble will be when slotted
            var angle = Mathf.Atan2(direction.y, direction.x);

            for (int i = 1; i < 3; i++)
            {
                var clockWiseDir = (Quaternion.AngleAxis(-60 * i, Vector3.forward) * direction).normalized;
                var counterClockWiseDir =
                    (Quaternion.AngleAxis(60 * i, Vector3.forward) * direction).normalized;
                // clock-wise
                var angleLeft = Mathf.Atan2(clockWiseDir.y, clockWiseDir.x);
                // counter clock-wise
                var angleRight = Mathf.Atan2(counterClockWiseDir.y, counterClockWiseDir.x);

                var indexedLeft = HasAvailableIndexAtAngle(collider.bubbleSlot, angleLeft, out var leftIndex);
                var indexedRight = HasAvailableIndexAtAngle(collider.bubbleSlot, angleRight, out var rightIndex);

                // if both are valid find the nearest to the original direction
                if (indexedLeft && indexedRight)
                {
                    var dotLeft = Vector3.Dot(direction, clockWiseDir);
                    var dotRight = Vector3.Dot(direction, counterClockWiseDir);

                    IndexWithCurrentAngle(gameEntity, collider.bubbleSlot,
                        dotLeft > dotRight ? angleLeft : angleRight);

                    Debug.LogFormat("Force indexed at {0}, with angle {1}",
                        dotLeft > dotRight ? leftIndex : rightIndex,
                        (dotLeft > dotRight ? angleLeft : angleRight) * Mathf.Rad2Deg);
                }
                else if (indexedLeft)
                {
                    IndexWithCurrentAngle(gameEntity, collider.bubbleSlot, angleLeft);

                    Debug.LogFormat("Force indexed at {0}, with angle {1}", leftIndex,
                        angleLeft * Mathf.Rad2Deg);
                }
                else if (indexedRight)
                {
                    IndexWithCurrentAngle(gameEntity, collider.bubbleSlot, angleRight);

                    Debug.LogFormat("Force indexed at {0}, with angle {1}", rightIndex,
                        angleRight * Mathf.Rad2Deg);
                }
            }
        }
    }

    private bool IndexWithCurrentAngle(GameEntity gameEntity, BubbleSlotComponent colliderSlot, float angle)
    {
        if (!HasAvailableIndexAtAngle(colliderSlot, angle, out var newSlotIndex)) return false;

        var finalPosition = newSlotIndex.IndexToPosition(_contexts.game, _configuration);

        // new slot to store this bubble
        gameEntity.ReplaceBubbleSlot(newSlotIndex);

        // move to final position
        gameEntity.ReplaceTranslateTo(_configuration.ProjectileSpeed, finalPosition);
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

        return true;
    }

    private bool HasAvailableIndexAtAngle(BubbleSlotComponent colliderSlot, float angle, out Vector2Int newSlotIndex)
    {
        // obtain new slot index from the collider slot position
        newSlotIndex = colliderSlot.CalculateSlotIndexAtAngle(angle);

        // check if this slot is already occupied we may be taking the wrong collider
        var indexer = _contexts.game.bubbleSlotIndexer.Value;

        if (indexer.ContainsKey(newSlotIndex))
        {
            return false;
        }

        return true;
    }

    public void OnTranslateToRemoved(GameEntity entity)
    {
        entity.isMoving = false;
        // let the merge system now take charge
        entity.isBubbleWaitingMerge = true;
        entity.isUnstableBubble = false;

        entity.RemoveTranslateToRemovedListener(this);
    }
}