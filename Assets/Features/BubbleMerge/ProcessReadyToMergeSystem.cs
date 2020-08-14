﻿using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// Once waiting for merge bubbles are properly marked this system 
/// takes care of finding which bubble all other bubbles will merge to
/// </summary>
public class ProcessReadyToMergeSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;

    public ProcessReadyToMergeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleReadyToMerge);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.isBubbleReadyToMerge && entity.hasBubbleNumber;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var maxPriority = 0;
        var matchingCount = entities.Count;
        var mergingNumber = entities[0].bubbleNumber.Value;

        var finalNumber = Mathf.Min(mergingNumber << (matchingCount - 1), 1 << _configuration.MaximumExponent);

        // find which entity is neighboring the final number, the system prioritizes further merges
        var matchingPriorities = new List<GameEntity>();

        foreach (var gameEntity in entities)
        {
            var neighbors = _contexts.game.GetBubbleNeighbors(gameEntity.bubbleSlot);

            var matchingFinalCount =
                neighbors.Count > 0 ? neighbors.Count(x => x.bubbleNumber.Value == finalNumber) : 0;

            // we found a neighbor that will guarantee a next merge
            if (matchingFinalCount > maxPriority)
            {
                matchingPriorities.Clear();
                maxPriority = matchingFinalCount;
            }

            if (matchingFinalCount == maxPriority)
            {
                matchingPriorities.Add(gameEntity);
            }
        }

        // for all the matching entities with the same priority chose the highest in world space
        maxPriority = int.MinValue;
        GameEntity choosenEntity = null;

        foreach (var matchingPriority in matchingPriorities)
        {
            if (matchingPriority.bubbleSlot.Value.y > maxPriority)
            {
                maxPriority = matchingPriority.bubbleSlot.Value.y;
                choosenEntity = matchingPriority;
            }
        }

        if (_contexts.game.hasBubbleChosenAsMergeTo)
        {
            _contexts.game.RemoveBubbleChosenAsMergeTo();
        }

        choosenEntity?.ReplaceBubbleChosenAsMergeTo(finalNumber);
    }
}