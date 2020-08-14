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
            var colliderSlotLink = colliderEntity.bubbleSlotLink.Value;
            var colliderSlotIndex = colliderSlotLink.bubbleSlot.Value;

            var newSlotIndex = CalculateNewSlotIndex(angle, colliderSlotIndex);

            // new slot to store this bubble
            var e = _contexts.game.CreateEntity();
            e.ReplaceBubbleSlot(newSlotIndex);
            // link between slot space and entity
            gameEntity.AddBubbleSlotLink(e);

            // todo: actually mark as stable when done merging
            var ins = _contexts.game.CreateEntity();
            ins.isBubbleProjectileInserted = true;
        }
    }

    private static Vector2Int CalculateNewSlotIndex(float angleRadians, in Vector2Int index)
    {
        var newIndex = new Vector2Int(index.x, index.y);
        var angle = Mathf.Rad2Deg * angleRadians;

        // right to the collider
        //  * *
        // * o (*)
        //  * *
        if (angle < 30 && angle >= -30)
        {
            newIndex.x++;
        }

        // top-right to the collider
        //  * (*)
        // *  o  *
        //  *   *
        if (angle >= 30 && angle < 90)
        {
            newIndex.x++;
            newIndex.y++;
        }

        // top-left to the collider
        //  (*) *
        // *  o  *
        //  *   *
        if (angle >= 90 && angle < 150)
        {
            newIndex.x--;
            newIndex.y++;
        }

        // left to the collider
        //    * *
        // (*) o *
        //    * *
        if (angle >= 150 && angle < -150)
        {
            newIndex.x--;
        }

        // bottom-left to the collider
        //  *  *
        // *  o  *
        // (*)  *
        if (angle >= -150 && angle < -90)
        {
            newIndex.x--;
            newIndex.y--;
        }

        // bottom-right to the collider
        //  *   *
        // *  o  *
        //  *  (*)
        if (angle >= -90 && angle < -30)
        {
            newIndex.x++;
            newIndex.y--;
        }

        return newIndex;
    }
}