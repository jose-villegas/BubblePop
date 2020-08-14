public class BubbleProjectileFeature : Feature
{
    public BubbleProjectileFeature(Contexts contexts)
    {
        Add(new BubbleProjectileSpawnSystem(contexts));
        Add(new BubbleDirectionSystem(contexts));
        Add(new BubbleShootSystem(contexts));
        Add(new BubbleBounceSystem(contexts));
        Add(new BubbleProjectileStopSystem(contexts));
        Add(new BubbleSlotterSystem(contexts));
    }
}