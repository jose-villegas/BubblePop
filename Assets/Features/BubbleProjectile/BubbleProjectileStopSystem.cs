using System.Linq;
using Entitas;
using UnityEngine;

/// <summary>
/// This system stops a projectile bubble as soon it hits another stable bubble
/// </summary>
public class BubbleProjectileStopSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private IGroup<GameEntity> _group;
    private IGameConfiguration _configuration;
    private int _bubblesLayer;

    public BubbleProjectileStopSystem(Contexts contexts)
    {
        _contexts = contexts;
        _configuration = _contexts.configuration.gameConfiguration.value;
        _bubblesLayer = LayerMask.GetMask("StableBubbles");
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Thrown));
    }

    public void Execute()
    {
        foreach (var gameEntity in _group.AsEnumerable().ToList())
        {
            // we using this function because, unity default trigger detection was all fuzzy, this detects immediately
            var colliders = Physics2D.OverlapCircleAll(gameEntity.position.Value, _configuration.OverlapCircleRadius,
                _bubblesLayer);

            foreach (var collider in colliders)
            {
                // determine if we have collision with an stable bubble
                var linkedView = collider.GetComponent<ILinkedView>();

                if (linkedView == null) return;

                var colliderEntity = (GameEntity)linkedView.LinkedEntity;

                // remove thrown and throwable components
                gameEntity.isThrown = false;

                if (gameEntity.hasDirection)
                {
                    gameEntity.RemoveDirection();
                }
                
                if (gameEntity.hasSpeed)
                {
                    gameEntity.RemoveSpeed();
                }

                Debug.Log("Ready for Slotter");

                // get collider linked entity - save collider data
                gameEntity.ReplaceCollidedWithBubble(colliderEntity);
            }
        }
    }
}