//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BubbleFallingComponent bubbleFalling { get { return (BubbleFallingComponent)GetComponent(GameComponentsLookup.BubbleFalling); } }
    public bool hasBubbleFalling { get { return HasComponent(GameComponentsLookup.BubbleFalling); } }

    public void AddBubbleFalling(UnityEngine.Vector3 newVelocity) {
        var index = GameComponentsLookup.BubbleFalling;
        var component = (BubbleFallingComponent)CreateComponent(index, typeof(BubbleFallingComponent));
        component.Velocity = newVelocity;
        AddComponent(index, component);
    }

    public void ReplaceBubbleFalling(UnityEngine.Vector3 newVelocity) {
        var index = GameComponentsLookup.BubbleFalling;
        var component = (BubbleFallingComponent)CreateComponent(index, typeof(BubbleFallingComponent));
        component.Velocity = newVelocity;
        ReplaceComponent(index, component);
    }

    public void RemoveBubbleFalling() {
        RemoveComponent(GameComponentsLookup.BubbleFalling);
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

    static Entitas.IMatcher<GameEntity> _matcherBubbleFalling;

    public static Entitas.IMatcher<GameEntity> BubbleFalling {
        get {
            if (_matcherBubbleFalling == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubbleFalling);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubbleFalling = matcher;
            }

            return _matcherBubbleFalling;
        }
    }
}
