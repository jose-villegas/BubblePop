using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Game Configuration", fileName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [SerializeField] private int _maximumExponent;
    [SerializeField] private int _maximumSpawnExponent;
    [SerializeField] private int _initialRowCount;
    [SerializeField] private Vector2 _bubblesSeparation;
    [SerializeField] private float _projectileBubblesHeight;
    [SerializeField] private int _slotsInitialVerticalIndex;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private Vector3 bubbleScale;
    [SerializeField] private Vector3 nextBubbleScale;
    [SerializeField] private float _overlapCircleRadius;
    [SerializeField] private float _mergeTranslateSpeed;
    [SerializeField] private float _reloadSpeed;

    public int MaximumExponent => _maximumExponent;

    public int MaximumSpawnExponent => _maximumSpawnExponent;

    public int InitialRowCount => _initialRowCount;

    public Vector2 BubblesSeparation => _bubblesSeparation;

    public float ProjectileBubblesHeight => _projectileBubblesHeight;

    public float ProjectileSpeed => _projectileSpeed;

    public int SlotsInitialVerticalIndex => _slotsInitialVerticalIndex;

    public Vector3 BubbleScale => bubbleScale;

    public Vector3 NextBubbleScale => nextBubbleScale;

    public float OverlapCircleRadius => _overlapCircleRadius;

    public float MergeTranslateSpeed => _mergeTranslateSpeed;

    public float ReloadSpeed => _reloadSpeed;
}