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

    public List<int> PossibleBubbleValues => _possibleBubbleValues;

    public int InitialRowCount => _initialRowCount;

    public Vector2 BubblesSeparation => _bubblesSeparation;

    public float ProjectileBubblesHeight => _projectileBubblesHeight;

    public int SlotsInitialVerticalIndex => _slotsInitialVerticalIndex;
}