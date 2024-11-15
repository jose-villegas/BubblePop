﻿using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleProjectileSpawnSystem : ReactiveSystem<GameEntity>, IAnyBubbleProjectileReloadListener,
    ITranslateToRemovedListener
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private readonly IGroup<GameEntity> _stableGroup;

    private GameEntity _nextBubble;
    private int _calls;

    public BubbleProjectileSpawnSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _stableGroup = _contexts.game.GetGroup(GameMatcher.StableBubble);

        // create reload listener
        var e = _contexts.game.CreateEntity();
        e.AddAnyBubbleProjectileReloadListener(this);
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
        CreateBubbleToThrow();
        CreateNextBubbleToThrow();
    }

    public void OnAnyBubbleProjectileReload(GameEntity entity)
    {
        // we don't need to reload if there is already a throwable bubble
        if (_contexts.game.GetGroup(GameMatcher.Throwable).count == 0)
        {
            // trigger scroll check behavior
            _contexts.game.isBubblesScrollCheck = true;

            // reload projectiles
            _nextBubble.ReplaceTranslateTo(_configuration.ReloadSpeed, Vector3.up * _configuration.ProjectileBubblesHeight);
            _nextBubble.ReplaceScaleTo(_configuration.ReloadSpeed, _configuration.BubbleScale);
            _nextBubble.AddTranslateToRemovedListener(this);
            _nextBubble.isMoving = true;
        }

        // remove reload entity
        entity.Destroy();
    }

    private void CreateNextBubbleToThrow()
    {
        _nextBubble = _contexts.game.CreateEntity();
        _nextBubble.isBubble = true;
        _nextBubble.isUnstableBubble = true;

        _nextBubble.ReplacePosition(Vector3.up * _configuration.ProjectileBubblesHeight + Vector3.left);
        _nextBubble.ReplaceScale(Vector3.zero);
        _nextBubble.ReplaceLayer(LayerMask.NameToLayer("FlyingBubble"));
        _nextBubble.ReplaceAsset("Bubble");

        // animate entry
        _nextBubble.AddScaleTo(_configuration.ReloadSpeed, _configuration.NextBubbleScale);
    }

    private void CreateBubbleToThrow()
    {
        // create bubble that will be thrown
        var e = _contexts.game.CreateEntity();
        e.isBubble = true;
        e.isThrowable = true;
        e.isUnstableBubble = true;

        e.ReplacePosition(Vector3.up * _configuration.ProjectileBubblesHeight + Vector3.left);
        e.ReplaceScale(Vector3.zero);
        e.ReplaceSpeed(_configuration.ProjectileSpeed);
        e.ReplaceLayer(LayerMask.NameToLayer("FlyingBubble"));
        e.ReplaceAsset("Bubble");

        // animate emtry
        e.ReplaceTranslateTo(_configuration.ReloadSpeed, Vector3.up * _configuration.ProjectileBubblesHeight);
        e.ReplaceScaleTo(_configuration.ReloadSpeed, _configuration.BubbleScale);
        e.isMoving = true;
    }

    public void OnTranslateToRemoved(GameEntity entity)
    {
        entity.isThrowable = true;
        entity.ReplaceSpeed(_configuration.ProjectileSpeed);
        CreateNextBubbleToThrow();

        entity.RemoveTranslateToRemovedListener(this);
    }
}