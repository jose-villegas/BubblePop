//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class AnyMouseUpEventSystem : Entitas.ReactiveSystem<InputEntity> {

    readonly Entitas.IGroup<InputEntity> _listeners;
    readonly System.Collections.Generic.List<InputEntity> _entityBuffer;
    readonly System.Collections.Generic.List<IAnyMouseUpListener> _listenerBuffer;

    public AnyMouseUpEventSystem(Contexts contexts) : base(contexts.input) {
        _listeners = contexts.input.GetGroup(InputMatcher.AnyMouseUpListener);
        _entityBuffer = new System.Collections.Generic.List<InputEntity>();
        _listenerBuffer = new System.Collections.Generic.List<IAnyMouseUpListener>();
    }

    protected override Entitas.ICollector<InputEntity> GetTrigger(Entitas.IContext<InputEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(InputMatcher.MouseUp)
        );
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasMouseUp;
    }

    protected override void Execute(System.Collections.Generic.List<InputEntity> entities) {
        foreach (var e in entities) {
            var component = e.mouseUp;
            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(listenerEntity.anyMouseUpListener.value);
                foreach (var listener in _listenerBuffer) {
                    listener.OnAnyMouseUp(e, component.value, component.button);
                }
            }
        }
    }
}
