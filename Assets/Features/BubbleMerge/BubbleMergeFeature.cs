public class BubbleMergeFeature : Feature
{
    public BubbleMergeFeature(Contexts contexts)
    {
        Add(new MergeMarkingSystem(contexts));
        Add(new ProcessReadyToMergeSystem(contexts));
        Add(new MergeWithChosenEntitySystem(contexts));
    }
}