using System;
using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This class scales entities with <see cref="ScaleToComponent"/>
/// once reached the target the component is removed
/// </summary>
public class ScaleToSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;

    public ScaleToSystem(Contexts contexts)
    {
        _contexts = contexts;
        _group = _contexts.game.GetGroup(GameMatcher.ScaleTo);
    }

    public void Execute()
    {
        foreach (var gameEntity in _group.AsEnumerable().ToList())
        {
            var scale = gameEntity.scale.Value;
            var speed = gameEntity.scaleTo.Speed;
            var target = gameEntity.scaleTo.Value;

            gameEntity.ReplaceScale(Vector3.Lerp(scale, target, Time.deltaTime * speed));

            if (Math.Abs((scale - target).sqrMagnitude) <= Constants.Tolerance)
            {
                gameEntity.RemoveScaleTo();
            }
        }
    }
}