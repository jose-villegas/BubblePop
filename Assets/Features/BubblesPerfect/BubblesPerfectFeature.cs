public class BubblesPerfectFeature : Feature
{
    public BubblesPerfectFeature(Contexts contexts)
    {
        Add(new BubblePerfectBoardRespawnSystem(contexts));
    }
}