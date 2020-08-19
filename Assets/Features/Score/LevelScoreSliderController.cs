using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LevelScoreSliderController : MonoBehaviour, IScoreListener
{
    private Slider _slider;
    private GameEntity scoreEntity;
    private IGameConfiguration _configuration;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        // recover persistent data
        var lastScore = PlayerPrefs.GetInt("Score", 0);

        var contexts = Contexts.sharedInstance;
        contexts.game.ReplaceScore(lastScore);

        _configuration = contexts.configuration.gameConfiguration.value;

        scoreEntity = contexts.game.scoreEntity;
        scoreEntity.AddScoreListener(this);

        OnScore(scoreEntity, lastScore);
    }

    public void OnScore(GameEntity entity, int value)
    {
        var index = _configuration.ScoreProgression.BinarySearch(value);

        if (index < 0)
        {
            index = ~index;
            index -= 1;
        }

        if (index < _configuration.ScoreProgression.Count)
        {
            var ceiling = 0;
            var minimum = 0;

            if (index + 1 < _configuration.ScoreProgression.Count)
            {
                ceiling = _configuration.ScoreProgression[index + 1];
                minimum = index < 0 ? 0 : _configuration.ScoreProgression[index];
            }
            else
            {
                var lastIndex = _configuration.ScoreProgression.Count - 1;
                ceiling = _configuration.ScoreProgression[lastIndex];
                minimum = _configuration.ScoreProgression[lastIndex - 1];
            }

            _slider.maxValue = ceiling;
            _slider.minValue = minimum;
            _slider.value = value;
        }
    }
}