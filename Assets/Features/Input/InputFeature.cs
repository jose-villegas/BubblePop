public class InputFeature : Feature
{
    public InputFeature(Contexts contexts)
    {
        Add(new MouseInputSystem(contexts));
        Add(new InputCleanupSystem(contexts));
    }
}