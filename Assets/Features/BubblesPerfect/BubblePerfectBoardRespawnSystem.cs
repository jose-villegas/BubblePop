using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This system creates the initial and assigns their slot
/// </summary>
public class BubblePerfectBoardRespawnSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;

    public BubblePerfectBoardRespawnSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubblePerfectBoard);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubblePerfectBoard;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //// replace offset as starting over
        //_contexts.game.ReplaceBubbleVerticalOffset(0);
        //// replace limits
        //_contexts.game.ReplaceBubbleSlotLimitsIndex(int.MinValue, int.MaxValue);

        //var configuration = _contexts.configuration.gameConfiguration.value;
        //var iterator = new BubbleSlotIterator(6, configuration.InitialRowCount);

        //foreach (Vector2Int slotIndex in iterator)
        //{
        //    // create initial bubbles
        //    _contexts.game.CreateStabeBubble(configuration, slotIndex);
        //}

        //_contexts.game.isBubblePerfectBoard = false;
    }
}