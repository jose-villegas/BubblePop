using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Game Configuration", fileName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [SerializeField] private List<int> _possibleBubbleValues;
    [SerializeField] private int initialRowCount;

    public List<int> PossibleBubbleValues => _possibleBubbleValues;

    public int InitialRowCount => initialRowCount;
}