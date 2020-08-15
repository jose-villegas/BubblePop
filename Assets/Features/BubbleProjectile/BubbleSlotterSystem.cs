using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// After a bubble is stopped and marked as unstable this system
/// links it to a new slot, yet unmerged
/// </summary>
public class BubbleSlotterSystem : ReactiveSystem<GameEntity>
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
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.UnstableBubble, GameMatcher.CollidedWithBubble));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isUnstableBubble && entity.hasCollidedWithBubble;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            var colliderEntity = gameEntity.collidedWithBubble.Value;

            var pos = gameEntity.position.Value;
            var colliderPos = colliderEntity.position.Value;
            var direction = (pos - colliderPos).normalized;

            // collision angle, used to determine where the bubble will be when slotted
            var angle = Mathf.Atan2(direction.y, direction.x);

            // obtain new slot index from the collider slot position
            var colliderSlot = colliderEntity.bubbleSlot;
            var newSlotIndex = colliderSlot.CalculateSlotIndexAtAngle(angle);

            // check if this slot is already occupied we may be taking the wrong collider
            var indexer = _contexts.game.bubbleSlotIndexer.Value;

            if (indexer.ContainsKey(newSlotIndex))
            {
                continue;
            }

            // check if the predicted index is out of bounds
            //if (newSlotIndex.x < 0 || newSlotIndex.x > 5) continue;

            var finalPosition = newSlotIndex.IndexToPosition(_contexts.game, _configuration);

            // new slot to store this bubble
            gameEntity.ReplaceBubbleSlot(newSlotIndex);

            // move to final position
            gameEntity.AddTranslateTo(_configuration.ProjectileSpeed, finalPosition);
            gameEntity.isMoving = true;

            // set to merge after movement
            gameEntity.OnComponentRemoved += OnDynamicsCompleted;

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
        }
    }

    private void OnDynamicsCompleted(IEntity entity, int index, IComponent component)
    {
        var gameEntity = (GameEntity) entity;

        if (component is TranslateToComponent)
        {
            gameEntity.isMoving = false;
            // let the merge system now take charge
            gameEntity.isBubbleWaitingMerge = true;
            gameEntity.isUnstableBubble = false;
        }

        entity.OnComponentRemoved -= OnDynamicsCompleted;
    }
}