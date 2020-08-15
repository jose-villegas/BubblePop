using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This class handles vertical scrolling for all the bubbles
/// </summary>
public class BubblesScrollingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private IGroup<GameEntity> _group;

    public BubblesScrollingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;

        _group = _contexts.game.GetGroup(GameMatcher.BubbleSlot);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubblesScrollCheck);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubblesScrollCheck;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // this event is unique, consume and destroy
        foreach (var gameEntity in entities)
        {
            gameEntity.Destroy();
        }

        var minimumPosition = float.MaxValue;

        foreach (var bubble in _group)
        {
            var pos = bubble.position.Value;

            if (pos.y <= minimumPosition)
            {
                minimumPosition = pos.y;
            }

            // in this case we need to scroll up
            if (pos.y <= _configuration.ScrollingBubblePositionBounds.x)
            {
                HandleScrollUpCase();
                return;
            }
        }

        // all the bubbles are above the minimum position - scroll down case
        if (minimumPosition >= _configuration.ScrollingBubblePositionBounds.y)
        {
            HandleScrollDownCase();
            return;
        }

        // we didn't find any matching case - simply reload
        var e = _contexts.game.CreateEntity();
        e.isBubbleProjectileReload = true;
    }

    private void HandleScrollDownCase()
    {
        HandleScroll(-1);
    }

    private void HandleScrollUpCase()
    {
        HandleScroll(1);
    }

    private void HandleScroll(float sign)
    {
        var subscribed = false;

        foreach (var bubble in _group)
        {
            var position = bubble.position.Value;
            bubble.ReplaceTranslateTo(_configuration.ScrollingSpeed,
                position + _configuration.BubblesSeparation.y * Vector3.up * sign);

            // a single subscription should suffice
            if (!subscribed)
            {
                subscribed = true;

                // wait for translation completion to activate the projectile reload
                bubble.OnComponentRemoved += OnDynamicsCompleted;
            }
        }

        var offset = _contexts.game.hasBubbleVerticalOffset ? _contexts.game.bubbleVerticalOffset.Value : 0;
        _contexts.game.ReplaceBubbleVerticalOffset(offset + sign * _configuration.BubblesSeparation.y);
    }

    private void OnDynamicsCompleted(IEntity entity, int index, IComponent component)
    {
        entity.OnComponentRemoved -= OnDynamicsCompleted;

        var e = _contexts.game.CreateEntity();
        e.isBubbleProjectileReload = true;
    }
}