using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GameStartBubblesSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;

    public GameStartBubblesSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    public GameStartBubblesSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public GameStartBubblesSystem(ICollector<GameEntity> collector) : base(collector)
    {
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
        var slots = _contexts.game.GetGroup(GameMatcher.BubbleSlot);
        var configuration = _contexts.configuration.gameConfiguration.value;

        var iterator = new BubbleSlotIterator(6, configuration.InitialRowCount);

        foreach (Vector2Int index in iterator)
        {
            // create initial bubbles
            var e = _contexts.game.CreateEntity();
            e.isBubble = true;
            e.AddAsset("Bubble");
            e.AddPosition(new Vector3(index.x, index.y));
        }
    }
}