using UnityEngine;

public class GameControllerBehaviour : MonoBehaviour
{
    [SerializeField] private GameConfiguration _configuration;

    private GameController _gameController;

    void Awake() => _gameController = new GameController(Contexts.sharedInstance, _configuration);
    void Start() => _gameController.Initialize();
    void Update() => _gameController.Execute();
}