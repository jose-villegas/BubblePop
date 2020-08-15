using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Game Configuration", fileName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [Header("Bubble Number")] [SerializeField]
    private int _maximumExponent;

    [SerializeField] private int _maximumSpawnExponent;

    [Header("Bubble Instancing")] [SerializeField]
    private int _initialRowCount;

    [Header("Bubble Transform")] [SerializeField]
    private float _slotPositioningVerticalHeight;

    [SerializeField] private Vector2 _bubblesSeparation;
    [SerializeField] private Vector3 bubbleScale;

    [Header("Bubble Merge")] [SerializeField]
    private float _mergeTranslateSpeed;

    [Header("Projectile Bubble")] [SerializeField]
    private float _reloadSpeed;

    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileBubblesHeight;
    [SerializeField] private Vector3 nextBubbleScale;
    [SerializeField] private float _overlapCircleRadius;

    [Header("Scrolling")] [SerializeField] private Vector2 _scrollingBubblePositionBounds;
    [SerializeField] private float _scrollingSpeed;

    public int MaximumExponent => _maximumExponent;

    public int MaximumSpawnExponent => _maximumSpawnExponent;

    public int InitialRowCount => _initialRowCount;

    public Vector2 BubblesSeparation => _bubblesSeparation;

    public float ProjectileBubblesHeight => _projectileBubblesHeight;

    public float ProjectileSpeed => _projectileSpeed;

    public float SlotPositioningVerticalHeight => _slotPositioningVerticalHeight;

    public Vector3 BubbleScale => bubbleScale;

    public Vector3 NextBubbleScale => nextBubbleScale;

    public float OverlapCircleRadius => _overlapCircleRadius;

    public float MergeTranslateSpeed => _mergeTranslateSpeed;

    public float ReloadSpeed => _reloadSpeed;

    public Vector2 ScrollingBubblePositionBounds => _scrollingBubblePositionBounds;

    public float ScrollingSpeed => _scrollingSpeed;
}