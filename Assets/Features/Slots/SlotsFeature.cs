    public class SlotsFeature : Feature
    {
        public SlotsFeature(Contexts contexts)
        {
            Add(new BubbleSlotInitializerSystem(contexts));
            Add(new BubbleSloIndexerSystem(contexts));
        }
    }
