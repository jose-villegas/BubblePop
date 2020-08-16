//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BubblePlayExplosionFXListenerComponent bubblePlayExplosionFXListener { get { return (BubblePlayExplosionFXListenerComponent)GetComponent(GameComponentsLookup.BubblePlayExplosionFXListener); } }
    public bool hasBubblePlayExplosionFXListener { get { return HasComponent(GameComponentsLookup.BubblePlayExplosionFXListener); } }

    public void AddBubblePlayExplosionFXListener(System.Collections.Generic.List<IBubblePlayExplosionFXListener> newValue) {
        var index = GameComponentsLookup.BubblePlayExplosionFXListener;
        var component = (BubblePlayExplosionFXListenerComponent)CreateComponent(index, typeof(BubblePlayExplosionFXListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBubblePlayExplosionFXListener(System.Collections.Generic.List<IBubblePlayExplosionFXListener> newValue) {
        var index = GameComponentsLookup.BubblePlayExplosionFXListener;
        var component = (BubblePlayExplosionFXListenerComponent)CreateComponent(index, typeof(BubblePlayExplosionFXListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBubblePlayExplosionFXListener() {
        RemoveComponent(GameComponentsLookup.BubblePlayExplosionFXListener);
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

    static Entitas.IMatcher<GameEntity> _matcherBubblePlayExplosionFXListener;

    public static Entitas.IMatcher<GameEntity> BubblePlayExplosionFXListener {
        get {
            if (_matcherBubblePlayExplosionFXListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubblePlayExplosionFXListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubblePlayExplosionFXListener = matcher;
            }

            return _matcherBubblePlayExplosionFXListener;
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

    public void AddBubblePlayExplosionFXListener(IBubblePlayExplosionFXListener value) {
        var listeners = hasBubblePlayExplosionFXListener
            ? bubblePlayExplosionFXListener.value
            : new System.Collections.Generic.List<IBubblePlayExplosionFXListener>();
        listeners.Add(value);
        ReplaceBubblePlayExplosionFXListener(listeners);
    }

    public void RemoveBubblePlayExplosionFXListener(IBubblePlayExplosionFXListener value, bool removeComponentWhenEmpty = true) {
        var listeners = bubblePlayExplosionFXListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveBubblePlayExplosionFXListener();
        } else {
            ReplaceBubblePlayExplosionFXListener(listeners);
        }
    }
}
