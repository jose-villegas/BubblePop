using System;
using UnityEngine;

[Serializable]
public class ExponentConfiguration
{
    [SerializeField] private Color _color;
    [SerializeField] private float _probability;

    public Color Color => _color;

    public float Probability => _probability;
}