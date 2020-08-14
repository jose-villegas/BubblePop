using System.Collections.Generic;
using DesperateDevs.Utils;
using Entitas;
using UnityEngine;

/// <summary>
/// Adds new slot instances to the indexer entity
/// </summary>
public class BubbleSloIndexerSystem : ReactiveSystem<GameEntity>, IDestroyedListener
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
            // in case its destroyed
            gameEntity.AddDestroyedListener(this);
        }
    }

    public void OnDestroyed(GameEntity entity)
    {
        var index = entity.bubbleSlot.Value;

        if (_indexerEntity.bubbleSlotIndexer.Value.ContainsKey(index))
        {
            _indexerEntity.bubbleSlotIndexer.Value.Remove(index);
        }
    }
}