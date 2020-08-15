using System;
using UnityEngine;

[Serializable]
public class ExponentConfiguration
{
    [SerializeField] private Color _color;

    public Color Color => _color;
}