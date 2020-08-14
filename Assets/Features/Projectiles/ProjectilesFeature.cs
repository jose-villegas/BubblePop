public class ProjectilesFeature : Feature
{
    public ProjectilesFeature(Contexts contexts)
    {
        Add(new ThrownSystem(contexts));
    }
}