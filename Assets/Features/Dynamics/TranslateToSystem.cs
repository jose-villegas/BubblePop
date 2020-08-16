using System;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This class moves entities with <see cref="TranslateToComponent"/>
/// once reached the target the component is removed
/// </summary>
public class TranslateToSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;

    public TranslateToSystem(Contexts contexts)
    {
        _contexts = contexts;
        _group = _contexts.game.GetGroup(GameMatcher.TranslateTo);
    }

    public void Execute()
    {
        foreach (var gameEntity in _group.AsEnumerable().ToList())
        {
            var position = gameEntity.position.Value;
            var speed = gameEntity.translateTo.Speed;
            var target = gameEntity.translateTo.Value;

            gameEntity.ReplacePosition(Vector3.Lerp(position, target, Time.deltaTime * speed));

            if (Math.Abs((position - target).sqrMagnitude) <= Constants.Tolerance)
            {
                gameEntity.ReplacePosition(target);
                gameEntity.RemoveTranslateTo();
                // trigger completion event
                gameEntity.isTranslateToCompleted = true;
            }
        }
    }
}