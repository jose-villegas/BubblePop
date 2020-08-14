public class BubblesScrollingFeature : Feature
{
    public BubblesScrollingFeature(Contexts contexts)
    {
        Add(new BubblesScrollingSystem(contexts));
    }
}