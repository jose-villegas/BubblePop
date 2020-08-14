using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LinkedViewController : MonoBehaviour, IUnityTransform, IPositionListener, IRotationListener,
    IScaleListener,
    IDestroyedListener
{
    public IEntity LinkedEntity { get; private set; }

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
        LinkedEntity = entity;

        var gameEntity = (GameEntity) LinkedEntity;

        gameEntity.AddDestroyedListener(this);
        gameEntity.AddPositionListener(this);
        gameEntity.AddRotationListener(this);
        gameEntity.AddScaleListener(this);
        gameEntity.AddLinkedView(this);

        if (gameEntity.hasPosition)
            transform.localPosition = gameEntity.position.Value;

        if (gameEntity.hasScale)
            transform.localScale = gameEntity.scale.Value;

        if (gameEntity.hasRotation)
            transform.rotation = gameEntity.rotation.Value;
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

        LinkedEntity.Destroy();
        Destroy(gameObject);
    }
}