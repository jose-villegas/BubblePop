using System.Linq;
using Entitas;
using Entitas.VisualDebugging.Unity;
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
        _bubblesLayer = LayerMask.GetMask("StableBubbles", "TopLimit");
        _group = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Thrown));
    }

    public void Execute()
    {
        foreach (var gameEntity in _group.AsEnumerable().ToList())
        {
            // shift the overlap circle for earlier detection, physics has issues on Update cycle
            var shift = gameEntity.direction.Value * _configuration.OverlapCircleRadius * Mathf.PI;

            // we using this function because, unity default trigger detection was all fuzzy, this detects immediately
            var colliders = Physics2D.OverlapCircleAll(gameEntity.position.Value + shift,
                _configuration.OverlapCircleRadius,
                _bubblesLayer);

            if (colliders.Length > 0)
            {
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

                gameEntity.isMoving = false;

                // check if we have reached the top limit
                if (colliders.Any(x => x.gameObject.layer == LayerMask.NameToLayer("TopLimit")))
                {
                    gameEntity.isDestroyed = true;

                    // trigger reload check
                    var e = _contexts.game.CreateEntity();
                    e.isBubbleProjectileReload = true;

                    return;
                }

                gameEntity.ReplaceCollidedWithBubbles(colliders
                    .Where(x => x.gameObject.layer == LayerMask.NameToLayer("StableBubbles"))
                    .Select(collider =>
                    {
                        // determine if we have collision with an stable bubble
                        var linkedView = collider.GetComponent<ILinkedView>();
                        return (GameEntity) linkedView.LinkedEntity;
                    }).ToArray());
            }
        }
    }
}