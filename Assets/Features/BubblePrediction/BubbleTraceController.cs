using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BubbleTraceController : MonoBehaviour, IBubbleTraceListener
{
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        // go ahead and create the trace entity beforehand
        var e = Contexts.sharedInstance.game.CreateEntity();
        e.ReplaceBubbleTrace(new List<Vector3>());

        // listen to modifications
        e.AddBubbleTraceListener(this);

        // link with entity
        gameObject.Link(e);
    }

    public void OnBubbleTrace(GameEntity entity, List<Vector3> values)
    {
        if (values == null || values.Count == 0)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        _lineRenderer.positionCount = values.Count;
        _lineRenderer.SetPositions(values.ToArray());
    }
}