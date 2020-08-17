using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubblePredictionHitSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly Contexts _contexts;
    private readonly IGameConfiguration _configuration;
    private GameEntity _prediction;

    public BubblePredictionHitSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubblePredictionHit);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubblePredictionHit;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            if (gameEntity.bubblePredictionHit.Value == null)
            {
                _prediction.ReplaceScaleTo(_configuration.ReloadSpeed, Vector3.zero);
                return;
            }

            var hitEntity = gameEntity.bubblePredictionHit.Value;

            if (hitEntity.hasBubbleFalling || !hitEntity.hasBubbleSlot) continue;

            var hit = gameEntity.bubblePredictionHit.Point;
            var direction = (hit - hitEntity.position.Value).normalized;
            // collision angle, used to determine where the bubble will be when slotted
            var angle = Mathf.Atan2(direction.y, direction.x);

            // obtain new slot index from the collider slot position
            var colliderSlot = hitEntity.bubbleSlot;
            var newSlotIndex = colliderSlot.CalculateSlotIndexAtAngle(angle);

            // check if this slot is already occupied
            var indexer = _contexts.game.bubbleSlotIndexer.Value;

            if (indexer.ContainsKey(newSlotIndex))
            {
                return;
            }

            // check if the predicted index is out of bounds
            if (newSlotIndex.x < 0 || newSlotIndex.x > 11) return;

            var finalPosition = newSlotIndex.IndexToPosition(_contexts.game, _configuration);

            // update prediction
            if (newSlotIndex == _prediction.bubbleSlot.Value) return;

            _prediction.ReplacePosition(finalPosition);
            _prediction.ReplaceScale(Vector3.zero);
            _prediction.ReplaceBubbleSlot(newSlotIndex);
            _prediction.ReplaceScaleTo(_configuration.ReloadSpeed, _configuration.BubbleScale);
        }
    }

    public void Initialize()
    {
        // instance _prediction asset
        _prediction = _contexts.game.CreateEntity();

        // add components
        _prediction.AddPosition(Vector3.zero);
        _prediction.AddScale(Vector3.zero);
        _prediction.AddAsset("Prediction");
        _prediction.AddBubbleSlot(Vector2Int.zero);
    }
}