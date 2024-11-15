//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity bubblesScrollCheckEntity { get { return GetGroup(GameMatcher.BubblesScrollCheck).GetSingleEntity(); } }

    public bool isBubblesScrollCheck {
        get { return bubblesScrollCheckEntity != null; }
        set {
            var entity = bubblesScrollCheckEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isBubblesScrollCheck = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly BubblesScrollCheckComponent bubblesScrollCheckComponent = new BubblesScrollCheckComponent();

    public bool isBubblesScrollCheck {
        get { return HasComponent(GameComponentsLookup.BubblesScrollCheck); }
        set {
            if (value != isBubblesScrollCheck) {
                var index = GameComponentsLookup.BubblesScrollCheck;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : bubblesScrollCheckComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBubblesScrollCheck;

    public static Entitas.IMatcher<GameEntity> BubblesScrollCheck {
        get {
            if (_matcherBubblesScrollCheck == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubblesScrollCheck);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubblesScrollCheck = matcher;
            }

            return _matcherBubblesScrollCheck;
        }
    }
}
