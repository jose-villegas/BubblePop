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
            // this bubble is being moved by another system, override
            if (bubble.hasTranslateTo) bubble.RemoveTranslateTo();

            var velocity = bubble.bubbleFalling.Velocity;
            bubble.ReplaceBubbleFalling(velocity + Vector3.down * Time.deltaTime * _configuration.FallingGravity);

            var position = bubble.position.Value;
            bubble.ReplacePosition(position + bubble.bubbleFalling.Velocity * Time.deltaTime);
            bubble.isMoving = true;

            var distanceToDeadZone = Mathf.Abs(position.y - _configuration.FallingDeadZoneHeight);

            if (distanceToDeadZone <= _configuration.FallingDissapearZoneDistance)
            {
                if (!bubble.hasScaleTo)
                {
                    bubble.ReplaceScaleTo(_configuration.MergeScaleSpeed, Vector3.zero);
                }

                if (!bubble.isBubblePlayDestroyFX)
                {
                    bubble.ReplacePlayAudio("bubble-pop");
                }

                bubble.isBubblePlayDestroyFX = true;
            }

            if (position.y < _configuration.FallingDeadZoneHeight)
            {
                bubble.isDestroyed = true;
            }
        }
    }
}