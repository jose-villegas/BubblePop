public class BubbleMergeFeature : Feature
{
    public BubbleMergeFeature(Contexts contexts)
    {
        Add(new BubbleFlaggingSystem(contexts));
        Add(new BubbleExplosionSystem(contexts));
        Add(new ProcessReadyToMergeSystem(contexts));
        Add(new MergeWithChosenEntitySystem(contexts));
    }
}