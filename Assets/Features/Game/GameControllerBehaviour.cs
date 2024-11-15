﻿using UnityEngine;

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

        var lineHeight0 = Vector3.up * _configuration.LinesHeight + Vector3.right * 10;
        var lineHeight1 = Vector3.up * _configuration.LinesHeight + Vector3.left * 10;
        Debug.DrawLine(lineHeight0, lineHeight1, Color.blue);

        var deadZone0 = Vector3.up * _configuration.FallingDeadZoneHeight + Vector3.right * 10;
        var deadZone1 = Vector3.up * _configuration.FallingDeadZoneHeight + Vector3.left * 10;
        Debug.DrawLine(deadZone0, deadZone1, Color.red);

        // aim angle
        var angle0 = new Vector3(Mathf.Cos(_configuration.AimAngleRange.x * Mathf.Deg2Rad),
            Mathf.Sin(_configuration.AimAngleRange.x * Mathf.Deg2Rad));
        var angle1 = new Vector3(Mathf.Cos(_configuration.AimAngleRange.y * Mathf.Deg2Rad),
            Mathf.Sin(_configuration.AimAngleRange.y * Mathf.Deg2Rad));

        var origin = Vector3.up * _configuration.ProjectileBubblesHeight;

        Debug.DrawLine(origin, origin + angle0 * 10, Color.yellow);
        Debug.DrawLine(origin, origin + angle1 * 10, Color.yellow);
    }
}