using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class BubbleWorldPositionSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;

    public BubbleWorldPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    public BubbleWorldPositionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public BubbleWorldPositionSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleSlotLink);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble && entity.hasBubbleSlotLink;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var screenWidth = Screen.width;
        var configuration = _contexts.configuration.gameConfiguration.value;

        foreach (var gameEntity in entities)
        {
            var slotLink = gameEntity.bubbleSlotLink.Value;
            var slot = slotLink.bubbleSlot.Value;

            var position = new Vector3
            (
                (slot.x - 5) * configuration.BubblesSeparation.x - configuration.BubblesSeparation.x / 2f,
                slot.y * configuration.BubblesSeparation.y,
                0
            );

            gameEntity.ReplacePosition(position);
        }
    }
}