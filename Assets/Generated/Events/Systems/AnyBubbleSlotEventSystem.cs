//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class AnyBubbleSlotEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly Entitas.IGroup<GameEntity> _listeners;
    readonly System.Collections.Generic.List<GameEntity> _entityBuffer;
    readonly System.Collections.Generic.List<IAnyBubbleSlotListener> _listenerBuffer;

    public AnyBubbleSlotEventSystem(Contexts contexts) : base(contexts.game) {
        _listeners = contexts.game.GetGroup(GameMatcher.AnyBubbleSlotListener);
        _entityBuffer = new System.Collections.Generic.List<GameEntity>();
        _listenerBuffer = new System.Collections.Generic.List<IAnyBubbleSlotListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.BubbleSlot)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasBubbleSlot;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.bubbleSlot;
            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(listenerEntity.anyBubbleSlotListener.value);
                foreach (var listener in _listenerBuffer) {
                    listener.OnAnyBubbleSlot(e, component.Value);
                }
            }
        }
    }
}