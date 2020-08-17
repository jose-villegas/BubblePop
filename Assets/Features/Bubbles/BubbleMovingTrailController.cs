using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BubbleMovingTrailController : MonoBehaviour, IMovingListener, IMovingRemovedListener
{
    private LinkedViewController _view;
    private TrailRenderer _trail;

    private void Awake()
    {
        _view = GetComponentInParent<LinkedViewController>();
        _trail = GetComponent<TrailRenderer>();

        _view.OnViewLinked += OnViewLinked;
    }

    private void OnViewLinked(GameEntity entity)
    {
        entity.AddMovingListener(this);
        entity.AddMovingRemovedListener(this);
    }

    public void OnMoving(GameEntity entity)
    {
        _trail.enabled = true;
    }

    public void OnMovingRemoved(GameEntity entity)
    {
        _trail.enabled = false;
    }
}
