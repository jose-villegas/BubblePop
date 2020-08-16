using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This system completes the merge process, merging all bubbles
/// that are ready to merge with the chosen bubble, the process is repeated
/// just in case the chosen bubble can also be merged
/// </summary>
public class MergeWithChosenEntitySystem : ReactiveSystem<GameEntity>, ITranslateToRemovedListener
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _mergeGroup;
    private IGameConfiguration _configuration;
    private int _expectedDestroyCount;

    public MergeWithChosenEntitySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _mergeGroup = _contexts.game.GetGroup(GameMatcher.BubbleWaitingMerge);
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleChosenAsMergeTo);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.hasBubbleChosenAsMergeTo;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var target = _contexts.game.bubbleChosenAsMergeToEntity;
        _expectedDestroyCount = _mergeGroup.count - 1;

        foreach (var readyBubble in _mergeGroup.AsEnumerable().ToList())
        {
            if (readyBubble == _contexts.game.bubbleChosenAsMergeToEntity) continue;

            // no longer waiting to be merged
            readyBubble.isBubbleWaitingMerge = false;

            readyBubble.ReplaceTranslateTo(_configuration.MergeTranslateSpeed, target.position.Value);
            readyBubble.ReplaceScaleTo(_configuration.MergeScaleSpeed, Vector3.zero);
            readyBubble.AddTranslateToRemovedListener(this);
            readyBubble.isBubblePlayFX = true;
            readyBubble.isMoving = true;

            // wait for translation complete to removed merged bubbles
            readyBubble.OnDestroyEntity += OnReadyBubbleDestroyed;
        }
    }

    private void OnReadyBubbleDestroyed(IEntity entity)
    {
        _expectedDestroyCount--;

        if (_expectedDestroyCount == 0)
        {
            // the chosen bubble is now stable
            var chosen = _contexts.game.bubbleChosenAsMergeToEntity;

            if (chosen != null && !chosen.hasBubbleFalling)
            {
                var finalNumber = chosen.bubbleChosenAsMergeTo.Value;
                chosen.ConvertToStableBubble();
                chosen.ReplaceBubbleNumber(finalNumber);

                // set chosen as waiting to merge, checking if there is further moves
                chosen.isBubbleWaitingMerge = true;
            }

            // trigger connection check
            _contexts.game.isBubbleConnectionCheck = true;
        }

        entity.OnDestroyEntity -= OnReadyBubbleDestroyed;
    }

    public void OnTranslateToRemoved(GameEntity entity)
    {

        entity.isDestroyed = true;
        entity.RemoveTranslateToRemovedListener(this);
    }
}