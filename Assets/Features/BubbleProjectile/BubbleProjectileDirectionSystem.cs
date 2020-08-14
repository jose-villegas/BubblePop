﻿using Entitas;
using UnityEngine;

/// <summary>
/// This system handles direction update for thrown and throwable
/// bubbles
/// </summary>
public class BubbleProjectileDirectionSystem : IExecuteSystem, IAnyGameStartedListener
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;
    private Camera _camera;

    public BubbleProjectileDirectionSystem(Contexts contexts)
    {
        _contexts = contexts;

        // throwable bubbles
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Throwable));

        // listener to game start
        var e = _contexts.game.CreateEntity();
        e.AddAnyGameStartedListener(this);
    }

    public void Execute()
    {
        if (_camera == null) return;

        var mousePos = Input.mousePosition;

        foreach (var bubble in _group)
        {
            var pos = bubble.position.Value;
        }
    }

    public void OnAnyGameStarted(GameEntity entity)
    {
        var cameraEntity = (GameEntity) _contexts.game.entityTagIndexer.Value["MainCamera"];

        if (cameraEntity != null)
        {
            var behavior = (MonoBehaviour) cameraEntity.linkedView.Value;
            _camera = behavior.GetComponent<Camera>();
        }
    }
}