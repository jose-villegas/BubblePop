public class BubblesFeature : Feature
{
    public BubblesFeature(Contexts contexts)
    {
        Add(new GameStartBubblesSystem(contexts));
        Add(new BubbleWorldPositionSystem(contexts));
        Add(new BubbleNumberingSystem(contexts));
        Add(new BubbleNudgeAnimationSystem(contexts));
    }
}