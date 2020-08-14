﻿using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Adds new slot instances to the indexer entity
/// </summary>
public class BubbleSloIndexerSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;

    private GameEntity _indexerEntity;

    public BubbleSloIndexerSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;

        // create indexer
        _indexerEntity = _contexts.game.CreateEntity();
        _indexerEntity.AddBubbleSlotIndexer(new Dictionary<Vector2Int, IEntity>());
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleSlot);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleSlot;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            // save to the indexer
            _indexerEntity.bubbleSlotIndexer.Value[gameEntity.bubbleSlot.Value] = gameEntity;
        }
    }
}