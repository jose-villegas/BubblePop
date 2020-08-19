using System.Linq;
using Entitas;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles input and marking bubbles as thrown
/// </summary>
public class BubbleShootSystem : IInitializeSystem, IAnyMouseUpListener, IAnyGameStartedListener
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private readonly IGroup<GameEntity> _group;

    private bool _gameStartedClick;

    public BubbleShootSystem(Contexts contexts)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;

        var e = _contexts.input.CreateEntity();
        e.AddAnyMouseUpListener(this);

        var l = _contexts.game.CreateEntity();
        l.AddAnyGameStartedListener(this);

        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Throwable));
    }

    public void Initialize()
    {
    }

    public void OnAnyMouseUp(InputEntity entity, Vector3 value, int button)
    {
        if (_gameStartedClick)
        {
            _gameStartedClick = false;
            return;
        }

        foreach (var gameEntity in _group.AsEnumerable().ToList())
        {
            var direction = gameEntity.direction.Value;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < _configuration.AimAngleRange.x || angle > _configuration.AimAngleRange.y)
            {
                return;
            }

            gameEntity.isThrowable = false;
            gameEntity.isThrown = true;
            gameEntity.isMoving = true;

            // play sfx
            _contexts.game.CreateEntity().AddPlayAudio("whoosh");
        }
    }

    public void OnAnyGameStarted(GameEntity entity)
    {
        _gameStartedClick = true;
    }
}