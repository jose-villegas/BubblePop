public class DynamicsFeature : Feature
{
    public DynamicsFeature(Contexts contexts)
    {
        Add(new TranslateToSystem(contexts));
        Add(new ScaleToSystem(contexts));
    }
}