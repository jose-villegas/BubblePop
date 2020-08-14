using Entitas;
using UnityEngine;

/// <summary>
/// This systems handles mouse events
/// </summary>
public class MouseInputSystem : IExecuteSystem
{
    private readonly Contexts _contexts;

    public MouseInputSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var e = _contexts.input.CreateEntity();

            e.isInputEvent = true;
            e.ReplaceMouseUp(Input.mousePosition, 0);
        }
    }
}