using UnityEngine;

/// <summary>
/// Handles special effects to bubbles
/// </summary>
public class BubbleFXController : MonoBehaviour, IBubbleNumberListener, IBubblePlayDestroyFXListener,
    IBubblePlayExplosionFXListener
{
    [SerializeField] private ParticleSystem _destroyFX;
    [SerializeField] private ParticleSystem _explosionFX;

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
        entity.AddBubblePlayDestroyFXListener(this);
        entity.AddBubblePlayExplosionFXListener(this);

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

    public void OnBubblePlayDestroyFX(GameEntity entity)
    {
        var ps = Instantiate(_destroyFX, transform.position, Quaternion.identity);

        ParticleSystem.MainModule main = ps.main;
        main.startColor = _color;
    }

    public void OnBubblePlayExplosionFX(GameEntity entity)
    {
        var ps = Instantiate(_explosionFX, transform.position, Quaternion.identity);

        ParticleSystem.MainModule main = ps.main;
        main.startColor = _color;
    }
}