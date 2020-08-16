using Entitas;
using UnityEngine;

/// <summary>
/// Handles falling bubbles
/// </summary>
public class BubbleFallingSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _group;
    private IGameConfiguration _configuration;

    public BubbleFallingSystem(Contexts contexts)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _group = contexts.game.GetGroup(GameMatcher.BubbleFalling);
    }

    public void Execute()
    {
        foreach (var bubble in _group)
        {
            var velocity = bubble.bubbleFalling.Velocity;
            bubble.ReplaceBubbleFalling(velocity + Vector3.down * Time.deltaTime * _configuration.FallingGravity);

            var position = bubble.position.Value;
            bubble.ReplacePosition(position + bubble.bubbleFalling.Velocity * Time.deltaTime);

            if (position.y < _configuration.FallingDeadZoneHeight)
            {
                bubble.isDestroyed = true;
            }
        }
    }
}