using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Once waiting for merge bubbles are properly marked
/// this system takes care of actually merging the bubbles
/// </summary>
public class ProcessReadyToMergeSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;

    public ProcessReadyToMergeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleReadyToMerge);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.isBubbleReadyToMerge;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var maxPriority = 0;
        var matchingCount = entities.Count;
        var mergingNumber = entities[0].bubbleNumber.Value;

        var finalNumber = Mathf.Min(mergingNumber << (matchingCount - 1), 1 << _configuration.MaximumExponent);

        Debug.Log(finalNumber);
    }
}