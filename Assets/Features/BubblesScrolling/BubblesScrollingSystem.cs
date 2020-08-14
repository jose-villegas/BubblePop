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
        return context.CreateCollector(GameMatcher.BubbleProjectileReload);
    }

    protected override bool Filter(GameEntity entity)
    {
        // don't care about filtering
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var bubble in _group)
        {
            // in this case we need to scroll up
            if (bubble.position.Value.y <= _configuration.MinimumBubblePosition)
            {
                HandleScrollUpCase();
                return;
            }
        }
    }

    private void HandleScrollUpCase()
    {
        foreach (var bubble in _group)
        {
            var position = bubble.position.Value;
            bubble.ReplaceTranslateTo(_configuration.ScrollingSpeed, position + _configuration.BubblesSeparation.y * Vector3.up);
        }

        var offset = _contexts.game.hasBubbleVerticalOffset ? _contexts.game.bubbleVerticalOffset.Value : 0;
        _contexts.game.ReplaceBubbleVerticalOffset(offset + _configuration.BubblesSeparation.y);
    }
}