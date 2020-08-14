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
        var configuration = _contexts.configuration.gameConfiguration.value;
        var iterator = new BubbleSlotIterator(6, configuration.InitialRowCount);

        foreach (Vector2Int slot in iterator)
        {
            var modifiedIndex = new Vector2Int(slot.x, slot.y + configuration.SlotsInitialVerticalIndex);
            // create initial bubbles
            var e = _contexts.game.CreateEntity();
            e.isBubble = true;
            e.isStableBubble = true;

            // add components
            e.AddPosition(Vector3.zero);
            e.AddScale(configuration.BubbleScale);
            e.AddLayer(LayerMask.NameToLayer("StableBubbles"));
            e.AddAsset("Bubble");

            // establish link with slot entity
            e.AddBubbleSlot(modifiedIndex);
        }
    }
}