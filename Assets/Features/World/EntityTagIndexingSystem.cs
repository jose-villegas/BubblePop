using System;
using System.Collections.Generic;
using Entitas;

/// <summary>
/// This systems just adds tagged entities to an indexer dictionary
/// </summary>
public class EntityTagIndexingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public EntityTagIndexingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;

        var e = _contexts.game.CreateEntity();
        e.AddEntityTagIndexer(new Dictionary<string, IEntity>());
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EntityTag);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEntityTag;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var tagIndexer = _contexts.game.entityTagIndexer.Value;

        foreach (var gameEntity in entities)
        {
            tagIndexer[gameEntity.entityTag.Value] = gameEntity;
        }
    }
}