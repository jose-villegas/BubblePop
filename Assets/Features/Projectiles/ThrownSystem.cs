using Entitas;
using UnityEngine;

public class ThrownSystem : IExecuteSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> _group;

    public ThrownSystem(Contexts contexts)
    {
        _contexts = contexts;
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Direction, GameMatcher.Thrown,
            GameMatcher.Position, GameMatcher.Speed));
    }

    public void Execute()
    {
        foreach (var gameEntity in _group)
        {
            var currentPos = gameEntity.position.Value;
            var direction = gameEntity.direction.Value;
            var speed = gameEntity.speed.Value;
            var nextPos = currentPos + direction * speed * Time.deltaTime;

            gameEntity.ReplacePosition(nextPos);
        }
    }
}