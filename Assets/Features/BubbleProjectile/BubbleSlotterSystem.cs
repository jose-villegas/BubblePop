using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// After a bubble is stopped and marked as unstable this system
/// links it to a new slot, yet unmerged
/// </summary>
public class BubbleSlotterSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public BubbleSlotterSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
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

            // new slot to store this bubble
            gameEntity.ReplaceBubbleSlot(newSlotIndex);

            // let the merge system now take charge
            gameEntity.isBubbleWaitingMerge = true;
            gameEntity.RemoveCollidedWithBubble();

            // // todo: actually mark as stable when done merging
            // gameEntity.ReplaceLayer(LayerMask.NameToLayer("StableBubbles"));
            // gameEntity.isStableBubble = true;

            // // send reload event
            // var e = _contexts.game.CreateEntity();
            // e.isBubbleProjectileReload = true;
        }
    }
}