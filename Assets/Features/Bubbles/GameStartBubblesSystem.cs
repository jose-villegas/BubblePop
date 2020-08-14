using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This system creates the initial and assigns their slot
/// </summary>
public class GameStartBubblesSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;

    public GameStartBubblesSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameStarted);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isGameEvent && entity.isGameStarted;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var availableInitialSlots = _contexts.game.GetGroup(GameMatcher.BubbleSlot);
        var configuration = _contexts.configuration.gameConfiguration.value;

        foreach (var availableInitialSlot in availableInitialSlots)
        {
            // create initial bubbles
            var e = _contexts.game.CreateEntity();
            e.isBubble = true;
            e.isStableBubble = true;

            // add components
            e.AddPosition(Vector3.zero);
            e.AddScale(configuration.BubbleScale);
            e.AddAsset("Bubble");

            // establish link with slot entity
            e.AddBubbleSlotLink(availableInitialSlot);
        }
    }
}