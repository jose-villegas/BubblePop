using System.Collections.Generic;
using System.Linq;
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

        _group = _contexts.game.GetGroup(GameMatcher.StableBubble);
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
        var maximumPosition = float.MinValue;
        var scrolled = false;

        foreach (var bubble in _group)
        {
            var pos = bubble.position.Value;

            if (pos.y <= minimumPosition)
            {
                minimumPosition = pos.y;
            }

            if (pos.y >= maximumPosition)
            {
                maximumPosition = pos.y;
            }

            // in this case we need to scroll up
            if (!scrolled && pos.y <= _configuration.ScrollingBubblePositionBounds.x)
            {
                HandleScrollUpCase();
                scrolled = true;
            }
        }

        if (!scrolled)
        {
            // all the bubbles are above the minimum position - scroll down case
            if (minimumPosition >= _configuration.ScrollingBubblePositionBounds.y)
            {
                HandleScrollDownCase();
            }
        }

        // meet line height requirement
        if (maximumPosition < _configuration.LinesHeight)
        {
            while (maximumPosition < _configuration.LinesHeight)
            {
                CreateNewLine();
                maximumPosition += _configuration.BubblesSeparation.y;
            }
        }
    }

    private void HandleScrollDownCase()
    {
        HandleScroll(-1);

        // create line up top
        CreateNewLine();
    }

    private void HandleScrollUpCase()
    {
        HandleScroll(1);

        // remove top-most line
        RemoveLine();
    }

    private void RemoveLine()
    {
        var limit = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
        var iterator = new BubbleSlotIterator(6, limit + 1, limit);

        foreach (Vector2Int slotIndex in iterator)
        {
            if (_contexts.game.bubbleSlotIndexer.Value.TryGetValue(slotIndex, out var entity))
            {
                var gameEntity = entity as GameEntity;
                gameEntity.isDestroyed = true;
            }
        }
    }

    private void CreateNewLine()
    {
        var limit = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
        var configuration = _contexts.configuration.gameConfiguration.value;
        var iterator = new BubbleSlotIterator(6, limit + 2, limit + 1);

        foreach (Vector2Int slotIndex in iterator)
        {
            // create initial bubbles
            _contexts.game.CreateStabeBubble(configuration, slotIndex);
        }
    }

    private void HandleScroll(float sign)
    {
        foreach (var bubble in _group)
        {
            var position = bubble.hasBubbleNudge ? bubble.bubbleNudge.Origin : bubble.position.Value;

            bubble.AddTranslateTo(_configuration.ScrollingSpeed,
                position + _configuration.BubblesSeparation.y * Vector3.up * sign);
        }

        var offset = _contexts.game.hasBubbleVerticalOffset ? _contexts.game.bubbleVerticalOffset.Value : 0;
        _contexts.game.ReplaceBubbleVerticalOffset(offset + sign * _configuration.BubblesSeparation.y);
    }
}