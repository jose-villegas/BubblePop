using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSlotIterator : IEnumerable
{
    private readonly int _horizontalSize;
    private readonly int _verticalSize;

    public BubbleSlotIterator(int horizontalSize, int verticalSize)
    {
        _horizontalSize = horizontalSize;
        _verticalSize = verticalSize;
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < _verticalSize; i++)
        {
            for (int j = 0; j < _horizontalSize; j++)
            {
                var horizontalIndex = j * 2 + i % 2;
                var verticalIndex = i;

                yield return new Vector2Int(horizontalIndex, verticalIndex);
            }
        }
    }
}
