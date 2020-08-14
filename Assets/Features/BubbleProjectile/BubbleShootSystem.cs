using Entitas;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles input and marking bubbles as thrown
/// </summary>
public class BubbleShootSystem : IInitializeSystem
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;

    public BubbleShootSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        var mouseUp = Input.GetMouseButtonUp(0);

        if (mouseUp)
        {
            foreach (var gameEntity in _group)
            {
                gameEntity.isThrowable = false;
                gameEntity.isThrown = true;
            }
        }
    }

    public void Initialize()
    {
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Throwable));
    }
}