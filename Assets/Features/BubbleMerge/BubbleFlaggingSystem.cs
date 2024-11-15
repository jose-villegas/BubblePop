using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This systems handles marking as waiting for merge 
/// all matching bubble entities, if the bubbles are
/// of maximum value this system marks them to be destroyed
/// as well its neighbors
/// </summary>
public class BubbleFlaggingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private readonly IGroup<GameEntity> _group;

    public BubbleFlaggingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _group = _contexts.game.GetGroup(GameMatcher.BubbleWaitingMerge);
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
            var maxNumber = 1 << _configuration.MaximumExponent;

            if (matchingNumber == maxNumber)
            {
                foreach (var waitingMerge in _group.AsEnumerable().ToList())
                {
                    waitingMerge.isBubbleWaitingMerge = false;
                    waitingMerge.isStableBubble = false;
                    waitingMerge.isBubbleConnected = false;
                    waitingMerge.isBubbleExplode = true;
                }
            }
            else if (_group.count > 1)
            {
                _contexts.game.ReplaceBubblesReadyToMerge(matchingNumber);
            }

            if (_group.count == 1)
            {
                foreach (var waitingMerge in _group.AsEnumerable().ToList())
                {
                    waitingMerge.ConvertToStableBubble();
                    waitingMerge.isBubbleWaitingMerge = false;
                }

                // trigger scroll check
                _contexts.game.isBubbleProjectileReload = true;
            }
        }
    }
}