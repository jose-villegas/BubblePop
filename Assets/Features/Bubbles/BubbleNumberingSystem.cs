using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Every new bubble needs a number assigned, this system handles that
/// </summary>
public class BubbleNumberingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;

    public BubbleNumberingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var item in entities)
        {
            var index = Random.Range(0, _configuration.PossibleBubbleValues.Count - 2);
            item.ReplaceBubbleNumber(_configuration.PossibleBubbleValues[index]);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBubble;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Bubble);
    }
}