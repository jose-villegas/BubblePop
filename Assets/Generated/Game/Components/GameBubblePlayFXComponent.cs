//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly BubblePlayFXComponent bubblePlayFXComponent = new BubblePlayFXComponent();

    public bool isBubblePlayFX {
        get { return HasComponent(GameComponentsLookup.BubblePlayFX); }
        set {
            if (value != isBubblePlayFX) {
                var index = GameComponentsLookup.BubblePlayFX;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : bubblePlayFXComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBubblePlayFX;

    public static Entitas.IMatcher<GameEntity> BubblePlayFX {
        get {
            if (_matcherBubblePlayFX == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubblePlayFX);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubblePlayFX = matcher;
            }

            return _matcherBubblePlayFX;
        }
    }
}
