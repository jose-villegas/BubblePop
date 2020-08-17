using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreLabelController : MonoBehaviour, IScoreListener
{
    private Text _label;
    private GameEntity scoreEntity;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }

    private void Start()
    {
        // recover persistent data
        var lastScore = PlayerPrefs.GetInt("Score", 0);

        var contexts = Contexts.sharedInstance;
        contexts.game.ReplaceScore(lastScore);

        scoreEntity = contexts.game.scoreEntity;
        scoreEntity.AddScoreListener(this);

        OnScore(scoreEntity, lastScore);
    }

    public void OnScore(GameEntity entity, int value)
    {
        _label.text = value.ToString("N0", CultureInfo.GetCultureInfo("en-GB"));
    }

    private void OnApplicationPause()
    {
        if (scoreEntity == null) return;

        PlayerPrefs.SetInt("Score", scoreEntity.score.Value);
        PlayerPrefs.Save();
    }
}