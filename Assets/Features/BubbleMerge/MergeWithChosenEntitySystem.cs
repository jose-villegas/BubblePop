using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This system completes the merge process, merging all bubbles
/// that are ready to merge with the chosen bubble
/// </summary>
public class MergeWithChosenEntitySystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _readyToMergeGroup;
    private IGameConfiguration _configuration;

    public MergeWithChosenEntitySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _readyToMergeGroup = _contexts.game.GetGroup(GameMatcher.BubbleReadyToMerge);
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleChosenAsMergeTo);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubbleReadyToMerge && entity.isBubble && entity.hasBubbleChosenAsMergeTo;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var target = _contexts.game.bubbleChosenAsMergeToEntity;

        if (_readyToMergeGroup.count > 1)
        {
            foreach (var readyBubble in _readyToMergeGroup.AsEnumerable().ToList())
            {
                if (readyBubble == target) continue;

                readyBubble.AddTranslateTo(_configuration.MergeTranslateSpeed, target.position.Value);
                readyBubble.OnComponentRemoved += OnDynamicsCompleted;
            }
        }
        else
        {
            foreach (var readyBubble in _readyToMergeGroup.AsEnumerable().ToList())
            {
                readyBubble.ConvertToStableBubble();
            }

            // trigger reload
            var e = _contexts.game.CreateEntity();
            e.isBubbleProjectileReload = true;
        }
    }

    private void OnDynamicsCompleted(IEntity entity, int index, IComponent component)
    {
        if (component is TranslateToComponent)
        {
            var gameEntity = (GameEntity) entity;
            gameEntity.isDestroyed = true;

            // the chosen bubble is now stable
            var chosen = _contexts.game.bubbleChosenAsMergeToEntity;

            if (chosen != null)
            {
                var finalNumber = chosen.bubbleChosenAsMergeTo.Value;
                chosen.ConvertToStableBubble();
                chosen.ReplaceBubbleNumber(finalNumber);

                // trigger reload
                var e = _contexts.game.CreateEntity();
                e.isBubbleProjectileReload = true;
            }
        }
    }
}