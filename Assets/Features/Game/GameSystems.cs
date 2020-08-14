public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        // view
        Add(new LinkedViewAssetInstancingSystem(contexts));
    }
}