using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BubbleNumberColorController : MonoBehaviour, IBubbleNumberListener
{
    private LinkedViewController _view;
    private SpriteRenderer _renderer;

    public void OnBubbleNumber(GameEntity entity, int value)
    {
        var exponent = (int) Mathf.Log(value, 2);
        var configuration = Contexts.sharedInstance.configuration.gameConfiguration.value;

        if (exponent > 0 && exponent <= configuration.ExponentConfigurations.Count)
        {
            var exponentConfig = configuration.ExponentConfigurations[exponent - 1];
            _renderer.color = exponentConfig.Color;
        }
    }

    private void Awake()
    {
        _view = GetComponent<LinkedViewController>();
        _renderer = GetComponent<SpriteRenderer>();

        _view.OnViewLinked += OnViewLinked;
    }

    private void OnViewLinked(GameEntity entity)
    {
        entity.AddBubbleNumberListener(this);

        if (!entity.hasBubbleNumber) return;


        OnBubbleNumber(entity, entity.bubbleNumber.Value);
    }
}