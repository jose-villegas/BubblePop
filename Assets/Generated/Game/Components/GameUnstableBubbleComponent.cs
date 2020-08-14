//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly UnstableBubbleComponent unstableBubbleComponent = new UnstableBubbleComponent();

    public bool isUnstableBubble {
        get { return HasComponent(GameComponentsLookup.UnstableBubble); }
        set {
            if (value != isUnstableBubble) {
                var index = GameComponentsLookup.UnstableBubble;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : unstableBubbleComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherUnstableBubble;

    public static Entitas.IMatcher<GameEntity> UnstableBubble {
        get {
            if (_matcherUnstableBubble == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UnstableBubble);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUnstableBubble = matcher;
            }

            return _matcherUnstableBubble;
        }
    }
}