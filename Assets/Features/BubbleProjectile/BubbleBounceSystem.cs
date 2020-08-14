using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This system detects when a thrown bubble collider with one of the bounding limits
/// changing its direction to bounce
/// </summary>
public class BubbleBounceSystem : ReactiveSystem<GameEntity>, IExecuteSystem, ITriggerEnter2DListener,
    ITriggerExit2DListener
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;

    public BubbleBounceSystem(Contexts contexts) : base(contexts.game)
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
            gameEntity.AddTriggerExit2DListener(this);
        }
    }

    public void OnTriggerEnter2D(GameEntity entity, Collider2D value)
    {
        var tag = value.tag;

        // we have collided with a limit
        if (tag == "LimitLeft" || tag == "LimitRight")
        {
            var currentDirection = entity.direction.Value;
            var newDirection = Vector3.Reflect(currentDirection, tag == "LimitRight" ? Vector3.left : Vector3.right);
            entity.ReplaceDirection(newDirection);
        }
    }

    public void OnTriggerExit2D(GameEntity entity, Collider2D value)
    {
        foreach (var gameEntity in _group)
        {
            gameEntity.RemoveTriggerEnter2D();
            gameEntity.RemoveTriggerExit2D();
        }
    }
}