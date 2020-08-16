using UnityEngine;

/// <summary>
/// Handles special effects to bubbles
/// </summary>
public class BubbleFXController : MonoBehaviour, IBubbleNumberListener, IBubblePlayFXListener
{
    [SerializeField] private ParticleSystem _destroyFX;

    private Color _color;
    private LinkedViewController _view;

    private void Awake()
    {
        _view = GetComponent<LinkedViewController>();

        _view.OnViewLinked += OnViewLinked;
    }

    private void OnViewLinked(GameEntity entity)
    {
        entity.AddBubbleNumberListener(this);
        entity.AddBubblePlayFXListener(this);

        if (!entity.hasBubbleNumber) return;

        // store the color for the bubble number
        OnBubbleNumber(entity, entity.bubbleNumber.Value);
    }

    public void OnBubbleNumber(GameEntity entity, int value)
    {
        var exponent = (int) Mathf.Log(value, 2);
        var configuration = Contexts.sharedInstance.configuration.gameConfiguration.value;

        if (exponent > 0 && exponent <= configuration.ExponentConfigurations.Count)
        {
            var exponentConfig = configuration.ExponentConfigurations[exponent - 1];
            _color = exponentConfig.Color;
        }
    }

    public void OnBubblePlayFX(GameEntity entity)
    {
        var ps = Instantiate(_destroyFX, transform.position, Quaternion.identity);

        ParticleSystem.MainModule main = ps.main;
        main.startColor = _color;
    }
}