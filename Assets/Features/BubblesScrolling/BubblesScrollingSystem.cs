﻿using System.Collections.Generic;
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
        bool scrolled = false;

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

        if (_group.count == 0)
        {
            var line = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
            CreateNewLine(line - 1);

            _contexts.game.isBubblePerfectBoard = true;
        }
        else
        {
            if (!scrolled)
            {
                // all the bubbles are above the minimum position - scroll down case
                if (minimumPosition >= _configuration.ScrollingBubblePositionBounds.y)
                {
                    HandleScrollDownCase();
                }
            }

            // there are not bubbles below the top-most line, clear board
            if (minimumPosition > _configuration.LinesHeight)
            {
                _contexts.game.isBubblePerfectBoard = true;
            }

            // we need to adapt the offset to match
            while (maximumPosition < _configuration.LinesHeight)
            {
                HandleScroll(1);
                maximumPosition += _configuration.BubblesSeparation.y;
            }
        }
    }

    private void HandleScrollDownCase()
    {
        HandleScroll(-1);

        // create line up top
        var line = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
        CreateNewLine(line);
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

    private void CreateNewLine(int limit)
    {
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
        var offset = _contexts.game.hasBubbleVerticalOffset ? _contexts.game.bubbleVerticalOffset.Value : 0;
        _contexts.game.ReplaceBubbleVerticalOffset(offset + sign * _configuration.BubblesSeparation.y);

        foreach (var bubble in _group)
        {
            bubble.ReplaceTranslateTo(_configuration.ScrollingSpeed,
                bubble.bubbleSlot.Value.IndexToPosition(_contexts.game, _configuration));
        }
    }
}