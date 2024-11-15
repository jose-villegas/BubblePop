﻿using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This systems determines from top to bottom which bubbles are disconnected
/// from the top most line
/// </summary>
public class BubbleDisconnectionCheckSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _stableBubbles;
    private readonly IGroup<GameEntity> _connectionChecks;
    private readonly IGameConfiguration _configuration;

    public BubbleDisconnectionCheckSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _stableBubbles = contexts.game.GetGroup(GameMatcher.StableBubble);
        _connectionChecks = contexts.game.GetGroup(GameMatcher.BubbleConnectionCheck);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleConnectionCheck);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubbleConnectionCheck;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // in this case we have received a connection check as trigger event
        // add top most bubbles to be checked
        foreach (var gameEntity in entities)
        {
            gameEntity.Destroy();
        }

        // mark all bubbles as disconnected bubbles
        foreach (var entity in _stableBubbles.AsEnumerable().ToList())
        {
            entity.isBubbleConnected = false;
        }

        var limit = _contexts.game.bubbleSlotLimitsIndex.MaximumVertical;
        var iterator = new BubbleSlotIterator(6, limit + 1, limit);

        foreach (Vector2Int slotIndex in iterator)
        {
            if (_contexts.game.bubbleSlotIndexer.Value.TryGetValue(slotIndex, out var entity))
            {
                var gameEntity = (GameEntity) entity;

                // since this is the top line, it's connected, we need to check for its neighbors
                TraverseConnectedNeighbors(gameEntity);
            }
        }

        // since we have traverse all the bubble structure, only disconnected bubbles will stay as disconnected
        MarkFallingBubbles();
    }

    private void MarkFallingBubbles()
    {
        // mark disconnected bubbles
        foreach (var entity in _stableBubbles.AsEnumerable().ToList())
        {
            if (!entity.isBubbleConnected)
            {
                var position = entity.position.Value;
                var scale = entity.scale.Value;
                var number = entity.bubbleNumber.Value;
                // just in case stills referenced
                entity.ReplaceLayer(LayerMask.NameToLayer("FallingBubbles"));
                entity.AddBubbleFalling(Vector3.zero);
                entity.isStableBubble = false;
                // destroy entity and create replacement
                entity.isDestroyed = true;

                // create substitute
                var replacement = _contexts.game.CreateEntity();

                replacement.AddAsset("Bubble");
                replacement.AddBubbleFalling(Random.insideUnitSphere.normalized * _configuration.FallingPushStrength);
                replacement.AddScale(scale);
                replacement.AddPosition(position);
                replacement.AddBubbleNumber(number);
                replacement.AddLayer(LayerMask.NameToLayer("FallingBubbles"));
            }
        }

        // trigger scroll check
        _contexts.game.isBubbleProjectileReload = true;
    }

    public void TraverseConnectedNeighbors(GameEntity entity)
    {
        entity.isBubbleConnected = true;

        var neighbors = _contexts.game.GetBubbleNeighbors(entity.bubbleSlot);

        foreach (var gameEntity in neighbors)
        {
            if (!gameEntity.isBubbleConnected) TraverseConnectedNeighbors(gameEntity);
        }
    }
}