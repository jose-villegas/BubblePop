//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class BubbleInsertedEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IBubbleInsertedListener> _listenerBuffer;

    public BubbleInsertedEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IBubbleInsertedListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.BubbleInserted)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isBubbleInserted && entity.hasBubbleInsertedListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.bubbleInsertedListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnBubbleInserted(e);
            }
        }
    }
}
