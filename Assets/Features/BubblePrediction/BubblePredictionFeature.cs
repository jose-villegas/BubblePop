public class BubblePredictionFeature : Feature
{
    public BubblePredictionFeature(Contexts contexts)
    {
        Add(new BubbleTracingSystem(contexts));
    }
}