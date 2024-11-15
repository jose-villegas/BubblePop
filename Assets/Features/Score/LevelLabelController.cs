﻿using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelLabelController : MonoBehaviour, IScoreListener
{
    [SerializeField] private bool _nextLevelLabel;

    private Text _label;
    private GameEntity scoreEntity;
    private IGameConfiguration _configuration;

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

        index += 1;
        _label.text = (_nextLevelLabel ? index + 1 : index).ToString("N0", CultureInfo.GetCultureInfo("en-GB"));
    }
}