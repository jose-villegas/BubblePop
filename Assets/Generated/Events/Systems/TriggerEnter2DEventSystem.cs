//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TriggerEnter2DEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ITriggerEnter2DListener> _listenerBuffer;

    public TriggerEnter2DEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ITriggerEnter2DListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.TriggerEnter2D)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasTriggerEnter2D && entity.hasTriggerEnter2DListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.triggerEnter2D;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.triggerEnter2DListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTriggerEnter2D(e, component.Value);
            }
        }
    }
}