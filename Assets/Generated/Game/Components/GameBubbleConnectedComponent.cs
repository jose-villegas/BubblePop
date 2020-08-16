//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly BubbleConnectedComponent bubbleConnectedComponent = new BubbleConnectedComponent();

    public bool isBubbleConnected {
        get { return HasComponent(GameComponentsLookup.BubbleConnected); }
        set {
            if (value != isBubbleConnected) {
                var index = GameComponentsLookup.BubbleConnected;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : bubbleConnectedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBubbleConnected;

    public static Entitas.IMatcher<GameEntity> BubbleConnected {
        get {
            if (_matcherBubbleConnected == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubbleConnected);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubbleConnected = matcher;
            }

            return _matcherBubbleConnected;
        }
    }
}