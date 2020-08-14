using System.Linq;
using Entitas;

/// <summary>
/// This systems cleans input entities
/// </summary>
public class InputCleanupSystem : ICleanupSystem
{
    private readonly Contexts _contexts;
    private IGroup<InputEntity> _group;

    public InputCleanupSystem(Contexts contexts)
    {
        _contexts = contexts;
        _group = _contexts.input.GetGroup(InputMatcher.InputEvent);

    }

    public void Cleanup()
    {
        foreach (var item in _group.AsEnumerable().ToList())
        {
            item.Destroy();
        }
    }
}