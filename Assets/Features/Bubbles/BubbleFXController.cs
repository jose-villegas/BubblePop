using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BubbleFXController : MonoBehaviour, IDestroyedListener
{
    private LinkedViewController _view;

    private void Awake()
    {
        _view = GetComponent<LinkedViewController>();

        _view.OnViewLinked += OnViewLinked;
    }

    private void OnViewLinked(GameEntity entity)
    {
        entity.AddDestroyedListener(this);
    }

    public void OnDestroyed(GameEntity entity)
    {
        Debug.Log(entity.bubbleNumber.Value);
    }
}