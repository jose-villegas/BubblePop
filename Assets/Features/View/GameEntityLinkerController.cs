public class GameEntityLinkerController : LinkedViewController
{
    private void Start()
    {
        var contexts = Contexts.sharedInstance.game;

        var e = contexts.CreateEntity();
        e.AddPosition(transform.localPosition);
        e.AddRotation(transform.localRotation);
        e.AddScale(transform.localScale);
        e.AddEntityTag(gameObject.tag);

        Link(e);
    }
}