using UnityEngine;

[RequireComponent(typeof(ILinkedView))]
public class TriggerReporterController : MonoBehaviour
{
    private ILinkedView _listener;

    private void Awake()
    {
        _listener = GetComponent<ILinkedView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var e = (GameEntity) _listener.LinkedEntity;

        if (!e.isEnabled) return;

        e.ReplaceTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var e = (GameEntity) _listener.LinkedEntity;

        if (!e.isEnabled) return;

        e.ReplaceTriggerExit2D(other);
    }
}