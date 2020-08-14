using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleProjectileSpawnSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private GameEntity _nextBubble;
    private IGameConfiguration _configuration;

    public BubbleProjectileSpawnSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameStarted);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isGameEvent && entity.isGameStarted;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // // reload projectiles
        // _nextBubble.AddTranslateTo(_configuration.ProjectileSpeed, Vector3.up * _configuration.ProjectileBubblesHeight);
        // _nextBubble.AddScaleTo(_configuration.ProjectileSpeed, _configuration.BubbleScale);

        // _nextBubble.OnComponentRemoved += OnDynamicsCompleted;

        CreateBubbleToThrow();
        CreateNextBubbleToThrow();
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

    private void CreateNextBubbleToThrow()
    {
        _nextBubble = _contexts.game.CreateEntity();
        _nextBubble.isBubble = true;
        _nextBubble.isUnstableBubble = true;

        _nextBubble.AddPosition(Vector3.up * _configuration.ProjectileBubblesHeight + Vector3.left);
        _nextBubble.AddScale(_configuration.NextBubbleScale);
        _nextBubble.AddLayer(LayerMask.NameToLayer("FlyingBubble"));
        _nextBubble.AddAsset("Bubble");
    }

    private void CreateBubbleToThrow()
    {
        // create bubble that will be thrown
        var e = _contexts.game.CreateEntity();
         e.isBubble = true;
        e.isThrowable = true;
        e.isUnstableBubble = true;

        e.AddPosition(Vector3.up * _configuration.ProjectileBubblesHeight);
        e.AddScale(_configuration.BubbleScale);
        e.AddSpeed(_configuration.ProjectileSpeed);
        e.AddLayer(LayerMask.NameToLayer("FlyingBubble"));
        e.AddAsset("Bubble");
    }
}