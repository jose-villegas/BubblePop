using System.Collections.Generic;
using Entitas;

/// <summary>
/// This system completes the merge process, merging all bubbles
/// that are ready to merge with the chosen bubble
/// </summary>
public class MergeWithChosenEntitySystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _readyToMergeGroup;

    public MergeWithChosenEntitySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _readyToMergeGroup = _contexts.game.GetGroup(GameMatcher.BubbleReadyToMerge);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleChosenAsMergeTo);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubbleReadyToMerge && entity.isBubble && entity.isBubbleChosenAsMergeTo;
    }

    protected override void Execute(List<GameEntity> entities)
    {

    }
}