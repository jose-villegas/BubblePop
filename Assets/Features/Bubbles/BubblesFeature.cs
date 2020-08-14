public class BubblesFeature : Feature
{
    public BubblesFeature(Contexts contexts)
    {
        Add(new GameStartBubblesSystem(contexts));
    }
}