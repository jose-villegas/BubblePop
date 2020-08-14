public class BubbleProjectileFeature : Feature
{
    public BubbleProjectileFeature(Contexts contexts)
    {
        Add(new BubbleProjectileSpawnSystem(contexts));
        Add(new BubbleDirectionSystem(contexts));
        Add(new BubbleShootSystem(contexts));
    }
}