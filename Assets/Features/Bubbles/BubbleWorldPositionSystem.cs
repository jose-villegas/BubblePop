using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This systems converts slot index to world space positions
/// </summary>
public class BubbleWorldPositionSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;

    public BubbleWorldPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleSlot);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.hasBubbleSlot && !entity.isMoving && !entity.isBubbleWaitingMerge;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var screenWidth = Screen.width;

        foreach (var gameEntity in entities)
        {
            var slot = gameEntity.bubbleSlot.Value;
            var position = slot.IndexToPosition(_contexts.game, _configuration);
            gameEntity.ReplacePosition(position);
        }
    }
}