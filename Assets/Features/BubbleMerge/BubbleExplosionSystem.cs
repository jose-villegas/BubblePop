using System.Collections.Generic;
using Entitas;

public class BubbleExplosionSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private readonly IGroup<GameEntity> _group;

    public BubbleExplosionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _group = _contexts.game.GetGroup(GameMatcher.BubbleWaitingMerge);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.BubbleExplode));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.isBubbleExplode;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            var neighbors = _contexts.game.GetBubbleNeighbors(gameEntity.bubbleSlot);

            foreach (var neighbor in neighbors)
            {
                neighbor.isBubblePlayExplosionFX = true;
                neighbor.isDestroyed = true;
            }

            gameEntity.isBubblePlayExplosionFX = true;
            gameEntity.isDestroyed = true;
        }

        _contexts.game.isBubbleConnectionCheck = true;
    }
}