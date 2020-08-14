public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        Add(new ViewFeature(contexts));
        Add(new BubblesFeature(contexts));
        Add(new BubbleProjectileFeature(contexts));
        Add(new SlotsFeature(contexts));
        Add(new WorldFeature(contexts));

        // events
        Add(new GameEventSystems(contexts));
    }
}