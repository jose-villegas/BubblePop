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
        return entity.isBubble && entity.hasBubbleSlot;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var screenWidth = Screen.width;
        var configuration = _contexts.configuration.gameConfiguration.value;
        var offset = _contexts.game.hasBubbleVerticalOffset ? _contexts.game.bubbleVerticalOffset.Value : 0;

        foreach (var gameEntity in entities)
        {
            var slot = gameEntity.bubbleSlot.Value;

            var position = new Vector3
            (
                (slot.x - 5) * configuration.BubblesSeparation.x - configuration.BubblesSeparation.x / 2f,
                slot.y * configuration.BubblesSeparation.y + _configuration.SlotPositioningVerticalHeight + offset,
                0
            );

            gameEntity.ReplacePosition(position);
        }
    }
}