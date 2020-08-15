using System.Collections.Generic;
using UnityEngine;

public static class GameContextExtensions
{
    public static List<GameEntity> GetBubbleNeighbors(this GameContext context, BubbleSlotComponent slot)
    {
        var neighbors = new List<GameEntity>();
        var slotIndexer = context.bubbleSlotIndexer.Value;

        for (int i = -1; i <= 1; i++)
        {
            var horizontalShift = 0;

            // top or bottom neighbors only increase horizontally by a single unit
            if (i == -1 || i == 1)
            {
                horizontalShift = 1;
            }
            else if (i == 0)
            {
                horizontalShift = 2;
            }

            var leftIndex = new Vector2Int(slot.Value.x - horizontalShift, slot.Value.y + i);
            var rightIndex = new Vector2Int(slot.Value.x + horizontalShift, slot.Value.y + i);

            if (slotIndexer.TryGetValue(leftIndex, out var leftNeighbor))
            {
                neighbors.Add((GameEntity) leftNeighbor);
            }

            if (slotIndexer.TryGetValue(rightIndex, out var rightNeighbor))
            {
                neighbors.Add((GameEntity) rightNeighbor);
            }
        }

        return neighbors;
    }

    public static Vector2Int CalculateSlotIndexAtAngle(this BubbleSlotComponent slot, float angleRads)
    {
        var newIndex = new Vector2Int(slot.Value.x, slot.Value.y);
        // map angle to 0 - 360
        var angle = (angleRads > 0 ? angleRads : (2 * Mathf.PI + angleRads)) * 360 / (2 * Mathf.PI);

        // right to the collider
        //  * *
        // * o (*)
        //  * *
        if (angle < 30 || angle >= 330)
        {
            newIndex.x += 2;
        }

        // top-right to the collider
        //  * (*)
        // *  o  *
        //  *   *
        if (angle >= 30 && angle < 90)
        {
            newIndex.x++;
            newIndex.y++;
        }

        // top-left to the collider
        //  (*) *
        // *  o  *
        //  *   *
        if (angle >= 90 && angle < 150)
        {
            newIndex.x--;
            newIndex.y++;
        }

        // left to the collider
        //    * *
        // (*) o *
        //    * *
        if (angle >= 150 && angle < 210)
        {
            newIndex.x -= 2;
        }

        // bottom-left to the collider
        //  *  *
        // *  o  *
        // (*)  *
        if (angle >= 210 && angle < 270)
        {
            newIndex.x--;
            newIndex.y--;
        }

        // bottom-right to the collider
        //  *   *
        // *  o  *
        //  *  (*)
        if (angle >= 270 && angle < 330)
        {
            newIndex.x++;
            newIndex.y--;
        }

        return newIndex;
    }

    public static void ConvertToStableBubble(this GameEntity bubble)
    {
        bubble.isBubbleWaitingMerge = false;
        bubble.isStableBubble = true;
        bubble.ReplaceLayer(LayerMask.NameToLayer("StableBubbles"));

        if (bubble.hasTranslateTo)
        {
            bubble.RemoveTranslateTo();
        }

        if (bubble.hasBubbleChosenAsMergeTo)
        {
            bubble.RemoveBubbleChosenAsMergeTo();
        }

        if (bubble.hasCollidedWithBubble)
        {
            bubble.RemoveCollidedWithBubble();
        }
    }

    public static Vector3 IndexToPosition(this Vector2Int slot, GameContext context, IGameConfiguration configuration)
    {
        var offset = context.hasBubbleVerticalOffset ? context.bubbleVerticalOffset.Value : 0;

        var position = new Vector3
        (
            (slot.x - 5) * configuration.BubblesSeparation.x - configuration.BubblesSeparation.x / 2f,
            slot.y * configuration.BubblesSeparation.y + configuration.SlotPositioningVerticalHeight + offset,
            0
        );
        return position;
    }

    public static void CreateStabeBubble(this GameContext context, IGameConfiguration configuration, Vector2Int slotIndex)
    {
        var e = context.CreateEntity();
        e.isBubble = true;
        e.isStableBubble = true;

        var scaleSpeed = Random.Range(configuration.SpawnScaleSpeedRange.x, configuration.SpawnScaleSpeedRange.y);
        // add components
        e.AddPosition(Vector3.zero);
        e.AddScale(Vector3.zero);
        e.AddScaleTo(scaleSpeed, configuration.BubbleScale);
        e.AddLayer(LayerMask.NameToLayer("StableBubbles"));
        e.AddAsset("Bubble");

        // establish link with slot entity
        e.AddBubbleSlot(slotIndex);
    }
}