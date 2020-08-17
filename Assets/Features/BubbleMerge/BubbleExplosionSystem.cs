using System.Collections.Generic;
using Entitas;

public class BubbleExplosionSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public BubbleExplosionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
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
                neighbor.isStableBubble = false;
                neighbor.isDestroyed = true;
            }

            gameEntity.ReplacePlayAudio("bubble-pop");
            gameEntity.isBubblePlayExplosionFX = true;
            gameEntity.isStableBubble = false;
            gameEntity.isDestroyed = true;
        }

        _contexts.game.isBubbleConnectionCheck = true;
    }
}