using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This system stops a projectile bubble as soon it hits another stable bubble
/// </summary>
public class BubbleProjectileStopSystem : ReactiveSystem<GameEntity>, ITriggerEnter2DListener
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;

    public BubbleProjectileStopSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;

        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Thrown));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Thrown));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.isThrown;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            gameEntity.AddTriggerEnter2DListener(this);
        }
    }

    public void OnTriggerEnter2D(GameEntity entity, Collider2D value)
    {
        var tag = value.tag;

        // determine if we have collision with an stable bubble
        var linkedView = value.GetComponent<ILinkedView>();

        if (linkedView == null) return;

        var colliderEntity = (GameEntity) linkedView.LinkedEntity;

        if (!colliderEntity.isStableBubble) return;

        // we have collided with a limit
        if (tag == "Bubble" && entity.isThrown)
        {
            // remove thrown and throwable components
            entity.isThrown = false;
            entity.RemoveDirection();
            entity.RemoveSpeed();

            // get collider linked entity - save collider data
            entity.ReplaceCollidedWithBubble(colliderEntity);
        }
    }
}