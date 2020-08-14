//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity bubbleSlotIndexerEntity { get { return GetGroup(GameMatcher.BubbleSlotIndexer).GetSingleEntity(); } }
    public BubbleSlotIndexerComponent bubbleSlotIndexer { get { return bubbleSlotIndexerEntity.bubbleSlotIndexer; } }
    public bool hasBubbleSlotIndexer { get { return bubbleSlotIndexerEntity != null; } }

    public GameEntity SetBubbleSlotIndexer(System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, GameEntity> newValue) {
        if (hasBubbleSlotIndexer) {
            throw new Entitas.EntitasException("Could not set BubbleSlotIndexer!\n" + this + " already has an entity with BubbleSlotIndexerComponent!",
                "You should check if the context already has a bubbleSlotIndexerEntity before setting it or use context.ReplaceBubbleSlotIndexer().");
        }
        var entity = CreateEntity();
        entity.AddBubbleSlotIndexer(newValue);
        return entity;
    }

    public void ReplaceBubbleSlotIndexer(System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, GameEntity> newValue) {
        var entity = bubbleSlotIndexerEntity;
        if (entity == null) {
            entity = SetBubbleSlotIndexer(newValue);
        } else {
            entity.ReplaceBubbleSlotIndexer(newValue);
        }
    }

    public void RemoveBubbleSlotIndexer() {
        bubbleSlotIndexerEntity.Destroy();
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

    public BubbleSlotIndexerComponent bubbleSlotIndexer { get { return (BubbleSlotIndexerComponent)GetComponent(GameComponentsLookup.BubbleSlotIndexer); } }
    public bool hasBubbleSlotIndexer { get { return HasComponent(GameComponentsLookup.BubbleSlotIndexer); } }

    public void AddBubbleSlotIndexer(System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, GameEntity> newValue) {
        var index = GameComponentsLookup.BubbleSlotIndexer;
        var component = (BubbleSlotIndexerComponent)CreateComponent(index, typeof(BubbleSlotIndexerComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBubbleSlotIndexer(System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, GameEntity> newValue) {
        var index = GameComponentsLookup.BubbleSlotIndexer;
        var component = (BubbleSlotIndexerComponent)CreateComponent(index, typeof(BubbleSlotIndexerComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBubbleSlotIndexer() {
        RemoveComponent(GameComponentsLookup.BubbleSlotIndexer);
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

    static Entitas.IMatcher<GameEntity> _matcherBubbleSlotIndexer;

    public static Entitas.IMatcher<GameEntity> BubbleSlotIndexer {
        get {
            if (_matcherBubbleSlotIndexer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubbleSlotIndexer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubbleSlotIndexer = matcher;
            }

            return _matcherBubbleSlotIndexer;
        }
    }
}
