using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PerfectNoticeController : MonoBehaviour, IAnyBubblePerfectBoardListener
{
    private Animator _animator;
    private GameEntity _listener;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        var contexts = Contexts.sharedInstance;
        _listener = contexts.game.CreateEntity();
        _listener.AddAnyBubblePerfectBoardListener(this);
    }

    public void OnAnyBubblePerfectBoard(GameEntity entity)
    {
        _animator.SetTrigger("Appear");
        entity.Destroy();
    }
}