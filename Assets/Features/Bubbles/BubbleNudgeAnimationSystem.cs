using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// When a bubble integrates into the bubbles, this system takes care of
/// processing feedback animations to the neighboring bubbles affected
/// </summary>
public class BubbleNudgeAnimationSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private IGroup<GameEntity> _group;

    public BubbleNudgeAnimationSystem(Contexts contexts)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;

        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.StableBubble,
            GameMatcher.BubbleNudge));
    }

    public void Execute()
    {
        foreach (var bubble in _group.AsEnumerable().ToList())
        {
            // avoid conflict with other moving systems
            if (bubble.hasTranslateTo)
            {
                bubble.RemoveBubbleNudge();
                continue;
            }

            var pos = bubble.position.Value;
            var srcPos = bubble.bubbleNudge.Origin;
            var target = srcPos + bubble.bubbleNudge.Direction * _configuration.NudgeDistance;

            if (bubble.bubbleNudge.Return)
            {
                target = bubble.bubbleNudge.Origin;
            }

            var newPos = Vector3.Lerp(pos, target, Time.deltaTime * _configuration.NudgeSpeed);
            bubble.ReplacePosition(newPos);

            // we need to return to the original position
            if (Mathf.Abs((newPos - target).sqrMagnitude) <= Constants.Tolerance)
            {
                bubble.ReplaceBubbleNudge(-bubble.bubbleNudge.Direction, bubble.bubbleNudge.Origin, true);
            }

            // once we are in return mode, if we reach the origin position remove from this system
            if (bubble.bubbleNudge.Return && Mathf.Abs((newPos - srcPos).sqrMagnitude) <= Constants.Tolerance)
            {
                bubble.ReplacePosition(srcPos);
                bubble.RemoveBubbleNudge();
            }
        }
    }
}