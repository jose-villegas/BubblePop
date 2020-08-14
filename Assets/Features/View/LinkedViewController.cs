using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LinkedViewController : MonoBehaviour, IUnityTransform, IPositionListener, IRotationListener,
    IScaleListener,
    IDestroyedListener
{
    protected GameEntity _entity;

    public virtual Vector3 Position
    {
        get => transform.localPosition;
        set => transform.localPosition = value;
    }

    public virtual Quaternion Rotation
    {
        get => transform.localRotation;
        set => transform.localRotation = value;
    }

    public virtual Vector3 Scale
    {
        get => transform.localScale;
        set => transform.localScale = value;
    }

    public void Link(IEntity entity)
    {
        gameObject.Link(entity);
        _entity = (GameEntity) entity;

        _entity.AddDestroyedListener(this);
        _entity.AddPositionListener(this);
        _entity.AddRotationListener(this);
        _entity.AddScaleListener(this);

        if (_entity.hasPosition)
            transform.localPosition = _entity.position.Value;

        if (_entity.hasScale)
            transform.localScale = _entity.scale.Value;

        if (_entity.hasRotation)
            transform.rotation = _entity.rotation.Value;
    }

    public void OnPosition(GameEntity entity, Vector3 value)
    {
        transform.localPosition = value;
    }

    public void OnRotation(GameEntity entity, Quaternion value)
    {
        transform.localRotation = value;
    }

    public void OnScale(GameEntity entity, Vector3 value)
    {
        transform.localScale = value;
    }

    public void OnDestroyed(GameEntity entity)
    {
        gameObject.Unlink();

        _entity.Destroy();
        Destroy(gameObject);
    }
}