public class ViewFeature : Feature
{
    public ViewFeature(Contexts contexts)
    {
        Add(new AssetInstancingSystem(contexts));
    }
}
