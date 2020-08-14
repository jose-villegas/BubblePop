using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleProjectileSpawnSystem : ReactiveSystem<GameEntity>, IAnyGameStartedListener
{
    private Contexts _contexts;

    public BubbleProjectileSpawnSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;

        var e = _contexts.game.CreateEntity();
        e.AddAnyGameStartedListener(this);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleInserted);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubbleInserted;
    }

    protected override void Execute(List<GameEntity> entities)
    {
    }

    public void OnAnyGameStarted(GameEntity entity)
    {
        CreateBubbleToThrow();
        CreateNextBubbleToThrow();
    }

    private void CreateNextBubbleToThrow()
    {
        var configuration = _contexts.configuration.gameConfiguration.value;
        var e = _contexts.game.CreateEntity();
        e.isBubble = true;

        e.AddAsset("Bubble");
        e.AddPosition(Vector3.up * configuration.ProjectileBubblesHeight + Vector3.left);
        e.AddScale(Vector3.one * 3);
    }

    private void CreateBubbleToThrow()
    {
        var configuration = _contexts.configuration.gameConfiguration.value;
        // create bubble that will be thrown
        var e = _contexts.game.CreateEntity();
        e.isThrowable = true;
        e.isBubble = true;

        e.AddAsset("Bubble");
        e.AddPosition(Vector3.up * configuration.ProjectileBubblesHeight);
        e.AddSpeed(configuration.ProjectileSpeed);
    }
}