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
    private IGroup<GameEntity> _group;
    private bool _gameStartedClick;

    public BubbleShootSystem(Contexts contexts)
    {
        _contexts = contexts;

        var e = _contexts.input.CreateEntity();
        e.AddAnyMouseUpListener(this);

        var l = _contexts.game.CreateEntity();
        l.AddAnyGameStartedListener(this);
    }

    public void Initialize()
    {
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Throwable));
    }

    public void OnAnyMouseUp(InputEntity entity, Vector3 value, int button)
    {
        // todo: carefully observe how this works on devices
        if (_gameStartedClick)
        {
            _gameStartedClick = false;
            return;
        }

        foreach (var gameEntity in _group.AsEnumerable().ToList())
        {
            gameEntity.isThrowable = false;
            gameEntity.isThrown = true;
        }
    }

    public void OnAnyGameStarted(GameEntity entity)
    {
        _gameStartedClick = true;
    }
}