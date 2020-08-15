using System;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class BubbleNumberController : MonoBehaviour, IBubbleNumberListener
{
    private LinkedViewController _view;
    private TextMesh _textMesh;

    public void OnBubbleNumber(GameEntity entity, int value)
    {
        // for bigger than 3 digits number add "K"
        _textMesh.text = value > 999 ? (value / 1000) + "K" :  value.ToString();
    }

    private void Awake() {
        _view = GetComponentInParent<LinkedViewController>();
        _textMesh = GetComponent<TextMesh>();

        _view.OnViewLinked += OnViewLinked;
    }

    private void OnViewLinked(GameEntity entity)
    {
        entity.AddBubbleNumberListener(this);

        if (!entity.hasBubbleNumber) return;

        OnBubbleNumber(entity, entity.bubbleNumber.Value);
    }
}