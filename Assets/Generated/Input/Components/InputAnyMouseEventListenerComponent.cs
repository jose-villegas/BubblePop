//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public AnyMouseEventListenerComponent anyMouseEventListener { get { return (AnyMouseEventListenerComponent)GetComponent(InputComponentsLookup.AnyMouseEventListener); } }
    public bool hasAnyMouseEventListener { get { return HasComponent(InputComponentsLookup.AnyMouseEventListener); } }

    public void AddAnyMouseEventListener(System.Collections.Generic.List<IAnyMouseEventListener> newValue) {
        var index = InputComponentsLookup.AnyMouseEventListener;
        var component = (AnyMouseEventListenerComponent)CreateComponent(index, typeof(AnyMouseEventListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyMouseEventListener(System.Collections.Generic.List<IAnyMouseEventListener> newValue) {
        var index = InputComponentsLookup.AnyMouseEventListener;
        var component = (AnyMouseEventListenerComponent)CreateComponent(index, typeof(AnyMouseEventListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyMouseEventListener() {
        RemoveComponent(InputComponentsLookup.AnyMouseEventListener);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherAnyMouseEventListener;

    public static Entitas.IMatcher<InputEntity> AnyMouseEventListener {
        get {
            if (_matcherAnyMouseEventListener == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.AnyMouseEventListener);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherAnyMouseEventListener = matcher;
            }

            return _matcherAnyMouseEventListener;
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
public partial class InputEntity {

    public void AddAnyMouseEventListener(IAnyMouseEventListener value) {
        var listeners = hasAnyMouseEventListener
            ? anyMouseEventListener.value
            : new System.Collections.Generic.List<IAnyMouseEventListener>();
        listeners.Add(value);
        ReplaceAnyMouseEventListener(listeners);
    }

    public void RemoveAnyMouseEventListener(IAnyMouseEventListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyMouseEventListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyMouseEventListener();
        } else {
            ReplaceAnyMouseEventListener(listeners);
        }
    }
}
