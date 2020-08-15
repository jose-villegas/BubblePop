using System.Collections.Generic;
using Entitas;
using TMPro.EditorUtilities;
using UnityEngine;

public class BubbleTracingSystem : IExecuteSystem, IAnyGameStartedListener
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _group;

    private Camera _camera;
    private int _hitLayer;

    public BubbleTracingSystem(Contexts contexts)
    {
        _contexts = contexts;

        // listener to game start
        var e = _contexts.game.CreateEntity();
        e.AddAnyGameStartedListener(this);

        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Throwable));
        _hitLayer = LayerMask.GetMask("Limits", "StableBubbles");
    }

    public void Execute()
    {
        // ignore until game started
        if (_camera == null) return;

        var mousePos = Input.mousePosition;

        // throwable position
        foreach (var bubble in _group)
        {
            var pos = bubble.position.Value;
            var screenPos = _camera.WorldToScreenPoint(pos);
            var direction = bubble.direction.Value;

            // cast ray in given direction
            var hit = Physics2D.Raycast(pos, direction, 20, _hitLayer);

            if (hit.collider != null)
            {
                var trace = new List<Vector3>() {pos};

                // limit the number of bounces, though it shouldn't be more than 2 usually
                for (int i = 0; i < 10; i++)
                {
                    if (hit.collider != null)
                    {
                        trace.Add(hit.point);

                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("StableBubbles"))
                        {
                            // obtain bubble entity
                            var e = hit.collider.GetComponent<ILinkedView>();
                            _contexts.game.ReplaceBubblePredictionHit(e.LinkedEntity as GameEntity, hit.point);
                            break;
                        }
                    }
                    else
                    {
                        trace = null;
                        _contexts.game.ReplaceBubblePredictionHit(null, direction);
                        break;
                    }

                    // reflect vector at hit point
                    direction = Vector3.Reflect(direction,
                        hit.collider.tag == "LimitRight" ? Vector3.left : Vector3.right);

                    hit = Physics2D.Raycast(hit.point, direction, 20, _hitLayer);
                }

                _contexts.game.ReplaceBubbleTrace(trace);

#if UNITY_EDITOR
                if (_contexts.game.bubbleTrace.Values != null && _contexts.game.bubbleTrace.Values.Count > 0)
                {
                    for (int i = 1; i < _contexts.game.bubbleTrace.Values.Count; i++)
                    {
                        var point0 = _contexts.game.bubbleTrace.Values[i - 1];
                        var point1 = _contexts.game.bubbleTrace.Values[i];

                        Debug.DrawLine(point0, point1);
                    }
                }
#endif
            }
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
