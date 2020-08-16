using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// Every new bubble needs a number assigned, this system handles that
/// </summary>
public class BubbleNumberingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private List<Vector2> _probabilitiesRanges;

    public BubbleNumberingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;

        var ceiling = _configuration.ExponentConfigurations.Sum(x => x.Probability);

        _probabilitiesRanges = new List<Vector2>();

        var lastCeiling = 0f;

        foreach (var exponentConfiguration in _configuration.ExponentConfigurations)
        {
            _probabilitiesRanges.Add(new Vector2(lastCeiling, lastCeiling + exponentConfiguration.Probability));
            lastCeiling += exponentConfiguration.Probability;
        }
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var item in entities)
        {
            var marker = Random.Range(0f, _probabilitiesRanges.Last().y);
            var exponent = 0;

            for (exponent = 0; exponent < _probabilitiesRanges.Count; exponent++)
            {
                var range = _probabilitiesRanges[exponent];

                if (marker >= range.x && marker <= range.y)
                {
                    exponent += 1;
                    break;
                }
            }

            item.AddBubbleNumber(1 << exponent);
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