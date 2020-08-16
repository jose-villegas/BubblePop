public class BubbleDisconnectFeature : Feature
{
    public BubbleDisconnectFeature(Contexts contexts)
    {
        Add(new BubbleFallingSystem(contexts));
        Add(new BubbleDisconnectionCheckSystem(contexts));
    }
}