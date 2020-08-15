using UnityEngine;

public class GameControllerBehaviour : MonoBehaviour
{
    [SerializeField] private GameConfiguration _configuration;

    private GameController _gameController;

    void Awake() => _gameController = new GameController(Contexts.sharedInstance, _configuration);
    void Start() => _gameController.Initialize();
    void Update() => _gameController.Execute();

    void OnDrawGizmos()
    {
        var topScrollBound0 = Vector3.up * _configuration.ScrollingBubblePositionBounds.y + Vector3.right * 10;
        var topScrollBound1 = Vector3.up * _configuration.ScrollingBubblePositionBounds.y + Vector3.left * 10;
        Debug.DrawLine(topScrollBound0, topScrollBound1, Color.cyan);

        var bottomScrollBound0 = Vector3.up * _configuration.ScrollingBubblePositionBounds.x + Vector3.right * 10;
        var bottomScrollBound1 = Vector3.up * _configuration.ScrollingBubblePositionBounds.x + Vector3.left * 10;
        Debug.DrawLine(bottomScrollBound0, bottomScrollBound1, Color.green);
    }
}