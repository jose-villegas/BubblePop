public class SlotsFeature : Feature
{
    public SlotsFeature(Contexts contexts)
    {
        Add(new BubbleSloIndexerSystem(contexts));
    }
}