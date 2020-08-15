public class BubblePredictionFeature : Feature
{
    public BubblePredictionFeature(Contexts contexts)
    {
        Add(new BubbleTracingSystem(contexts));
        Add(new BubblePredictionHitSystem(contexts));
    }
}