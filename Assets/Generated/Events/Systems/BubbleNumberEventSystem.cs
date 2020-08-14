//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class BubbleNumberEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IBubbleNumberListener> _listenerBuffer;

    public BubbleNumberEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IBubbleNumberListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.BubbleNumber)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasBubbleNumber && entity.hasBubbleNumberListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.bubbleNumber;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.bubbleNumberListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnBubbleNumber(e, component.Value);
            }
        }
    }
}