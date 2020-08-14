using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Game Configuration", fileName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [SerializeField] private List<int> _possibleBubbleValues;
    [SerializeField] private int _initialRowCount;
    [SerializeField] private Vector2 _bubblesSeparation;
    [SerializeField] private float _projectileBubblesHeight;
    [SerializeField] private int _slotsInitialVerticalIndex;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private Vector3 bubbleScale;
    [SerializeField] private Vector3 nextBubbleScale;
    [SerializeField] private float _overlapCircleRadius;

    public List<int> PossibleBubbleValues => _possibleBubbleValues;

    public int InitialRowCount => _initialRowCount;

    public Vector2 BubblesSeparation => _bubblesSeparation;

    public float ProjectileBubblesHeight => _projectileBubblesHeight;

    public float ProjectileSpeed => _projectileSpeed;

    public int SlotsInitialVerticalIndex => _slotsInitialVerticalIndex;

    public Vector3 BubbleScale => bubbleScale;

    public Vector3 NextBubbleScale => nextBubbleScale;

    public float OverlapCircleRadius => _overlapCircleRadius;
}