//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity bubblePerfectBoardEntity { get { return GetGroup(GameMatcher.BubblePerfectBoard).GetSingleEntity(); } }

    public bool isBubblePerfectBoard {
        get { return bubblePerfectBoardEntity != null; }
        set {
            var entity = bubblePerfectBoardEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isBubblePerfectBoard = true;
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

    static readonly BubblePerfectBoardComponent bubblePerfectBoardComponent = new BubblePerfectBoardComponent();

    public bool isBubblePerfectBoard {
        get { return HasComponent(GameComponentsLookup.BubblePerfectBoard); }
        set {
            if (value != isBubblePerfectBoard) {
                var index = GameComponentsLookup.BubblePerfectBoard;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : bubblePerfectBoardComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBubblePerfectBoard;

    public static Entitas.IMatcher<GameEntity> BubblePerfectBoard {
        get {
            if (_matcherBubblePerfectBoard == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubblePerfectBoard);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubblePerfectBoard = matcher;
            }

            return _matcherBubblePerfectBoard;
        }
    }
}
