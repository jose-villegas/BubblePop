public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        Add(new ViewFeature(contexts));
        Add(new BubblesFeature(contexts));
        Add(new BubbleProjectileFeature(contexts));
        Add(new SlotsFeature(contexts));
        Add(new WorldFeature(contexts));
        Add(new InputFeature(contexts));
        Add(new ProjectilesFeature(contexts));
        Add(new DynamicsFeature(contexts));

        // events
        Add(new GameEventSystems(contexts));
        Add(new InputEventSystems(contexts));
    }
}