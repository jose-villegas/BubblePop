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

        foreach (Vector2Int slotIndex in iterator)
        {
            // create initial bubbles
            _contexts.game.CreateStabeBubble(configuration, slotIndex);
        }
    }
}