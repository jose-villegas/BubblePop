using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This systems handles marking as waiting for merge 
/// all matching bubble entities
/// </summary>
public class MergeMarkingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public MergeMarkingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var item in entities)
        {
            var neighbors = _contexts.game.GetBubbleNeighbors(item.bubbleSlot);

            // check which ones match the merge number
            foreach (var neighbor in neighbors)
            {
                if (neighbor.bubbleNumber == item.bubbleNumber)
                {
                    // mark also as waiting for merge
                    neighbor.isBubbleWaitingMerge = true;
                }
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.isBubbleWaitingMerge;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleWaitingMerge);
    }
}