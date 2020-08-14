using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// This system detects when a thrown bubble collider with one of the bounding limits
/// changing its direction to bounce
/// </summary>
public class BubbleBounceSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;
    private int _limitsLayer;
    private IGameConfiguration _configuration;

    public BubbleBounceSystem(Contexts contexts)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _limitsLayer = LayerMask.GetMask("Limits");
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Thrown));
    }

    public void Execute()
    {
        foreach (var gameEntity in _group)
        {
            // we using this function because, unity default trigger detection was all fuzzy, this detects immediately
            var colliders = Physics2D.OverlapCircleAll(gameEntity.position.Value, _configuration.OverlapCircleRadius,
                _limitsLayer);

            foreach (var collider in colliders)
            {
                // we have collided with a limit
                if (collider.tag != "LimitLeft" && collider.tag != "LimitRight") continue;

                var currentDirection = gameEntity.direction.Value;
                var newDirection = Vector3.Reflect(currentDirection,
                    collider.tag == "LimitRight" ? Vector3.left : Vector3.right);
                gameEntity.ReplaceDirection(newDirection);
            }
        }
    }
}