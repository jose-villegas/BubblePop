﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Game Configuration", fileName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [Header("Bubble Number")] [SerializeField]
    private int _maximumExponent;

    [SerializeField] private List<ExponentConfiguration> _exponentConfigurations;

    [Header("Bubble Instancing")] [SerializeField]
    private int _initialRowCount;

    [SerializeField] private Vector2 _spawnScaleSpeedRange;

    [Header("Bubble Transform")] [SerializeField]
    private float _slotPositioningVerticalHeight;

    [SerializeField] private Vector2 _bubblesSeparation;
    [SerializeField] private Vector3 bubbleScale;

    [Header("Bubble Merge")] [SerializeField]
    private float _mergeTranslateSpeed;

    [SerializeField] private float _mergeScaleSpeed;

    [Header("Projectile Bubble")] [SerializeField]
    private Vector2 _aimAngleRange;

    [SerializeField] private float _reloadSpeed;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileBubblesHeight;
    [SerializeField] private Vector3 nextBubbleScale;
    [SerializeField] private float _overlapCircleRadius;


    [Header("Scrolling")] [SerializeField] private Vector2 _scrollingBubblePositionBounds;
    [SerializeField] private float _scrollingSpeed;
    [SerializeField] private float _linesHeight;


    [Header("Feedback")] [SerializeField] private float _nudgeDistance;

    [SerializeField] private float _nudgeSpeed;
    [SerializeField] private float _circleCastRadius;

    [Header("Falling Bubbles")] [SerializeField]
    private float _fallingDeadZoneHeight;

    [SerializeField] private float _fallingDissapearZoneHeight;

    [SerializeField] private float _fallingGravity;
    [SerializeField] private float _fallingPushStrength;

    [Header("Progression")] [SerializeField]
    private List<int> _scoreProgression;

    [Header("Display")] [SerializeField] 
    private GameDisplayConfiguration _displayConfiguration;


    public List<ExponentConfiguration> ExponentConfigurations => _exponentConfigurations;

    public int MaximumExponent => _maximumExponent;

    public int InitialRowCount => _initialRowCount;

    public Vector2 BubblesSeparation => _bubblesSeparation;

    public float ProjectileBubblesHeight => _projectileBubblesHeight;

    public float ProjectileSpeed => _projectileSpeed;

    public float SlotPositioningVerticalHeight => _slotPositioningVerticalHeight;

    public Vector3 BubbleScale => bubbleScale;

    public Vector3 NextBubbleScale => nextBubbleScale;

    public float OverlapCircleRadius => _overlapCircleRadius;

    public float MergeTranslateSpeed => _mergeTranslateSpeed;

    public float MergeScaleSpeed => _mergeScaleSpeed;

    public float ReloadSpeed => _reloadSpeed;

    public Vector2 ScrollingBubblePositionBounds => _scrollingBubblePositionBounds;

    public float LinesHeight => _linesHeight;

    public float ScrollingSpeed => _scrollingSpeed;

    public float NudgeDistance => _nudgeDistance;

    public float NudgeSpeed => _nudgeSpeed;

    public float CircleCastRadius => _circleCastRadius;

    public float FallingDeadZoneHeight => _fallingDeadZoneHeight;

    public float FallingDissapearZoneDistance => _fallingDissapearZoneHeight;

    public float FallingGravity => _fallingGravity;

    public float FallingPushStrength => _fallingPushStrength;

    public Vector2 AimAngleRange => _aimAngleRange;

    public List<int> ScoreProgression => _scoreProgression;

    public GameDisplayConfiguration DisplayConfiguration => _displayConfiguration;

    public Vector2 SpawnScaleSpeedRange => _spawnScaleSpeedRange;
}