using UnityEngine;

public class GameControllerBehaviour : MonoBehaviour
{
    private GameController _gameController;

    void Awake() => _gameController = new GameController(Contexts.sharedInstance);
    void Start() => _gameController.Initialize();
    void Update() => _gameController.Execute();
}