//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BubbleTraceListenerComponent bubbleTraceListener { get { return (BubbleTraceListenerComponent)GetComponent(GameComponentsLookup.BubbleTraceListener); } }
    public bool hasBubbleTraceListener { get { return HasComponent(GameComponentsLookup.BubbleTraceListener); } }

    public void AddBubbleTraceListener(System.Collections.Generic.List<IBubbleTraceListener> newValue) {
        var index = GameComponentsLookup.BubbleTraceListener;
        var component = (BubbleTraceListenerComponent)CreateComponent(index, typeof(BubbleTraceListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBubbleTraceListener(System.Collections.Generic.List<IBubbleTraceListener> newValue) {
        var index = GameComponentsLookup.BubbleTraceListener;
        var component = (BubbleTraceListenerComponent)CreateComponent(index, typeof(BubbleTraceListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBubbleTraceListener() {
        RemoveComponent(GameComponentsLookup.BubbleTraceListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherBubbleTraceListener;

    public static Entitas.IMatcher<GameEntity> BubbleTraceListener {
        get {
            if (_matcherBubbleTraceListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubbleTraceListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubbleTraceListener = matcher;
            }

            return _matcherBubbleTraceListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddBubbleTraceListener(IBubbleTraceListener value) {
        var listeners = hasBubbleTraceListener
            ? bubbleTraceListener.value
            : new System.Collections.Generic.List<IBubbleTraceListener>();
        listeners.Add(value);
        ReplaceBubbleTraceListener(listeners);
    }

    public void RemoveBubbleTraceListener(IBubbleTraceListener value, bool removeComponentWhenEmpty = true) {
        var listeners = bubbleTraceListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveBubbleTraceListener();
        } else {
            ReplaceBubbleTraceListener(listeners);
        }
    }
}
