using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleProjectileSpawnSystem : ReactiveSystem<GameEntity>, IAnyGameStartedListener
{
    private Contexts _contexts;
    private GameEntity _nextBubble;
    private IGameConfiguration _configuration;

    public BubbleProjectileSpawnSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;

        var e = _contexts.game.CreateEntity();
        e.AddAnyGameStartedListener(this);

        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleProjectileInserted);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubbleProjectileInserted;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // reload projectiles
        _nextBubble.AddTranslateTo(_configuration.ProjectileSpeed, Vector3.up * _configuration.ProjectileBubblesHeight);
        _nextBubble.AddScaleTo(_configuration.ProjectileSpeed, _configuration.BubbleScale);

        _nextBubble.OnComponentRemoved += OnDynamicsCompleted;
    }

    private void OnDynamicsCompleted(IEntity entity, int index, IComponent component)
    {
        _nextBubble.OnComponentRemoved -= OnDynamicsCompleted;

        if (component is TranslateToComponent || component is ScaleToComponent)
        {
            _nextBubble.isThrowable = true;
            _nextBubble.AddSpeed(_configuration.ProjectileSpeed);

            CreateNextBubbleToThrow();
        }
    }

    public void OnAnyGameStarted(GameEntity entity)
    {
        CreateBubbleToThrow();
        CreateNextBubbleToThrow();
    }

    private void CreateNextBubbleToThrow()
    {
        _nextBubble = _contexts.game.CreateEntity();
        _nextBubble.isBubble = true;
        _nextBubble.isUnstableBubble = true;

        _nextBubble.AddAsset("Bubble");
        _nextBubble.AddPosition(Vector3.up * _configuration.ProjectileBubblesHeight + Vector3.left);
        _nextBubble.AddScale(_configuration.NextBubbleScale);
    }

    private void CreateBubbleToThrow()
    {
        // create bubble that will be thrown
        var e = _contexts.game.CreateEntity();
        e.isThrowable = true;
        e.isBubble = true;
        e.isUnstableBubble = true;

        e.AddAsset("Bubble");
        e.AddPosition(Vector3.up * _configuration.ProjectileBubblesHeight);
        e.AddScale(_configuration.BubbleScale);
        e.AddSpeed(_configuration.ProjectileSpeed);
    }
}