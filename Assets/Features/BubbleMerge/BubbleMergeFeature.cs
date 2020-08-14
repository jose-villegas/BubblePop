public class BubbleMergeFeature : Feature
{
    public BubbleMergeFeature(Contexts contexts)
    {
        Add(new MergeWithChosenEntitySystem(contexts));
        Add(new ProcessReadyToMergeSystem(contexts));
        Add(new MergeMarkingSystem(contexts));
    }
}