public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        Add(new BubbleProjectileFeature(contexts));
        Add(new BubblesFeature(contexts));
        Add(new SlotsFeature(contexts));
        Add(new BubbleMergeFeature(contexts));
        Add(new WorldFeature(contexts));
        Add(new InputFeature(contexts));
        Add(new ProjectilesFeature(contexts));
        Add(new DynamicsFeature(contexts));
        Add(new ViewFeature(contexts));

        // events
        Add(new GameEventSystems(contexts));
        Add(new InputEventSystems(contexts));
    }
}