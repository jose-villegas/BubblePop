using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleTracingSystem : IExecuteSystem, IAnyGameStartedListener
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private readonly IGroup<GameEntity> _group;

    private Camera _camera;
    private int _hitLayer;

    public BubbleTracingSystem(Contexts contexts)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;

        // listener to game start
        var e = _contexts.game.CreateEntity();
        e.AddAnyGameStartedListener(this);

        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Throwable));
        _hitLayer = LayerMask.GetMask("Limits", "StableBubbles", "TopLimit");
    }

    public void Execute()
    {
        // ignore until game started
        if (_camera == null) return;

        var mousePos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            _contexts.game.ReplaceBubbleTrace(null);
            _contexts.game.ReplaceBubblePredictionHit(null, Vector3.zero);
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        // throwable position
        foreach (var bubble in _group)
        {
            var pos = bubble.position.Value;
            var screenPos = _camera.WorldToScreenPoint(pos);
            var direction = bubble.direction.Value;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < _configuration.AimAngleRange.x || angle > _configuration.AimAngleRange.y)
            {
                _contexts.game.ReplaceBubbleTrace(null);
                _contexts.game.ReplaceBubblePredictionHit(null, Vector3.zero);
                return;
            }

            // cast ray in given direction, 
            var circleCast = Physics2D.CircleCast(pos, _configuration.CircleCastRadius, direction, 15, _hitLayer);
            var shift = Vector3.zero;
            var lastTag = string.Empty;

            if (circleCast.collider != null)
            {
                var trace = new List<Vector3>() {pos};

                // limit the number of bounces, though it shouldn't be more than 2 usually
                for (int i = 0; i < 10; i++)
                {
                    if (circleCast.collider != null)
                    {
                        var hitBubble = circleCast.collider.gameObject.layer == LayerMask.NameToLayer("StableBubbles");
                        var hitCeiling = circleCast.collider.gameObject.layer == LayerMask.NameToLayer("TopLimit");

                        if (hitBubble)
                        {
                            // shift trace position for a bit more precise end point
                            var centroidShift = direction * _configuration.CircleCastRadius * Mathf.PI / 4;
                            trace.Add(circleCast.centroid + new Vector2(centroidShift.x, centroidShift.y));
                            // obtain bubble entity
                            var e = circleCast.collider.GetComponent<ILinkedView>();
                            _contexts.game.ReplaceBubblePredictionHit(e.LinkedEntity as GameEntity, circleCast.point);
                            break;
                        }

                        trace.Add(circleCast.point);

                        if (hitCeiling) break;
                    }
                    else
                    {
                        trace = null;
                        _contexts.game.ReplaceBubblePredictionHit(null, Vector3.zero);
                        break;
                    }

                    // don't reflect twice on the same limit
                    if (lastTag != circleCast.collider.tag)
                    {
                        // reflect vector at hit point
                        if (circleCast.collider.tag == "LimitRight")
                        {
                            lastTag = circleCast.collider.tag;
                            direction = Vector3.Reflect(direction, Vector3.left).normalized;
                            shift = Vector3.zero;
                        }
                        else if (circleCast.collider.tag == "LimitLeft")
                        {
                            lastTag = circleCast.collider.tag;
                            direction = Vector3.Reflect(direction, Vector3.right).normalized;
                            shift = Vector3.zero;
                        }
                    }

                    pos = circleCast.centroid;
                    shift += direction * _configuration.CircleCastRadius * Mathf.PI / 2f;

                    circleCast = Physics2D.CircleCast(pos + shift, _configuration.CircleCastRadius, direction, 15,
                        _hitLayer);
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