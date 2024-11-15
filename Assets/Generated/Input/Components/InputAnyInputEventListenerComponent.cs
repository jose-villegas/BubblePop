//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public AnyInputEventListenerComponent anyInputEventListener { get { return (AnyInputEventListenerComponent)GetComponent(InputComponentsLookup.AnyInputEventListener); } }
    public bool hasAnyInputEventListener { get { return HasComponent(InputComponentsLookup.AnyInputEventListener); } }

    public void AddAnyInputEventListener(System.Collections.Generic.List<IAnyInputEventListener> newValue) {
        var index = InputComponentsLookup.AnyInputEventListener;
        var component = (AnyInputEventListenerComponent)CreateComponent(index, typeof(AnyInputEventListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyInputEventListener(System.Collections.Generic.List<IAnyInputEventListener> newValue) {
        var index = InputComponentsLookup.AnyInputEventListener;
        var component = (AnyInputEventListenerComponent)CreateComponent(index, typeof(AnyInputEventListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyInputEventListener() {
        RemoveComponent(InputComponentsLookup.AnyInputEventListener);
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

    static Entitas.IMatcher<InputEntity> _matcherAnyInputEventListener;

    public static Entitas.IMatcher<InputEntity> AnyInputEventListener {
        get {
            if (_matcherAnyInputEventListener == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.AnyInputEventListener);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherAnyInputEventListener = matcher;
            }

            return _matcherAnyInputEventListener;
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

    public void AddAnyInputEventListener(IAnyInputEventListener value) {
        var listeners = hasAnyInputEventListener
            ? anyInputEventListener.value
            : new System.Collections.Generic.List<IAnyInputEventListener>();
        listeners.Add(value);
        ReplaceAnyInputEventListener(listeners);
    }

    public void RemoveAnyInputEventListener(IAnyInputEventListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyInputEventListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyInputEventListener();
        } else {
            ReplaceAnyInputEventListener(listeners);
        }
    }
}
