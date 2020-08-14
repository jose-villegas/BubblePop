public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        Add(new ViewFeature(contexts));
        Add(new SlotsFeature(contexts));
    }
}