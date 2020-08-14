public class BubbleMergeFeature : Feature
{
    public BubbleMergeFeature(Contexts contexts)
    {
        Add(new MergeMarkingSystem(contexts));
    }
}