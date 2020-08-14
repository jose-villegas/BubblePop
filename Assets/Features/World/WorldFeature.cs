public class WorldFeature : Feature
{
    public WorldFeature(Contexts contexts)
    {
        Add(new EntityTagIndexingSystem(contexts));
    }
}