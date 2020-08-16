using System.Collections.Generic;
using System.Linq;
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

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.isBubbleWaitingMerge && entity.hasBubbleNumber && entity.hasBubbleSlot;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleWaitingMerge);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var foundMatch = false;
        var matchingNumber = 0;

        foreach (var item in entities)
        {
            var neighbors = _contexts.game.GetBubbleNeighbors(item.bubbleSlot);
            matchingNumber = item.bubbleNumber.Value;

            // check which ones match the merge number
            foreach (var neighbor in neighbors)
            {
                if (neighbor.bubbleNumber.Value == item.bubbleNumber.Value && !neighbor.isBubbleWaitingMerge)
                {
                    // mark also as waiting for merge
                    neighbor.isBubbleWaitingMerge = true;
                    foundMatch = true;
                }
            }
        }

        // since there is no more matching neighbors, it's ready for merging
        if (!foundMatch)
        {
            var e = _contexts.game.CreateEntity();
            e.ReplaceBubblesReadyToMerge(matchingNumber);
        }
    }
}