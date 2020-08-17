using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LevelScoreSliderController : MonoBehaviour, IScoreListener
{
    [SerializeField] private bool _nextLevelLabel;

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
        }

        if (index < _configuration.ScoreProgression.Count)
        {
            var ceiling = 0;

            if (index > 0)
            {
                ceiling = _configuration.ScoreProgression[index - 1];
            }

            _slider.maxValue = _configuration.ScoreProgression[index] - ceiling;
            _slider.value = value - ceiling;
        }
    }
}