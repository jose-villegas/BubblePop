using Entitas;

public class ThrownSystem : IExecuteSystem
{
    private Contexts _contexts;

    public ThrownSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
    }
}