//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity bubblesReadyToMergeEntity { get { return GetGroup(GameMatcher.BubblesReadyToMerge).GetSingleEntity(); } }
    public BubblesReadyToMergeComponent bubblesReadyToMerge { get { return bubblesReadyToMergeEntity.bubblesReadyToMerge; } }
    public bool hasBubblesReadyToMerge { get { return bubblesReadyToMergeEntity != null; } }

    public GameEntity SetBubblesReadyToMerge(int newValue) {
        if (hasBubblesReadyToMerge) {
            throw new Entitas.EntitasException("Could not set BubblesReadyToMerge!\n" + this + " already has an entity with BubblesReadyToMergeComponent!",
                "You should check if the context already has a bubblesReadyToMergeEntity before setting it or use context.ReplaceBubblesReadyToMerge().");
        }
        var entity = CreateEntity();
        entity.AddBubblesReadyToMerge(newValue);
        return entity;
    }

    public void ReplaceBubblesReadyToMerge(int newValue) {
        var entity = bubblesReadyToMergeEntity;
        if (entity == null) {
            entity = SetBubblesReadyToMerge(newValue);
        } else {
            entity.ReplaceBubblesReadyToMerge(newValue);
        }
    }

    public void RemoveBubblesReadyToMerge() {
        bubblesReadyToMergeEntity.Destroy();
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

    public BubblesReadyToMergeComponent bubblesReadyToMerge { get { return (BubblesReadyToMergeComponent)GetComponent(GameComponentsLookup.BubblesReadyToMerge); } }
    public bool hasBubblesReadyToMerge { get { return HasComponent(GameComponentsLookup.BubblesReadyToMerge); } }

    public void AddBubblesReadyToMerge(int newValue) {
        var index = GameComponentsLookup.BubblesReadyToMerge;
        var component = (BubblesReadyToMergeComponent)CreateComponent(index, typeof(BubblesReadyToMergeComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBubblesReadyToMerge(int newValue) {
        var index = GameComponentsLookup.BubblesReadyToMerge;
        var component = (BubblesReadyToMergeComponent)CreateComponent(index, typeof(BubblesReadyToMergeComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBubblesReadyToMerge() {
        RemoveComponent(GameComponentsLookup.BubblesReadyToMerge);
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

    static Entitas.IMatcher<GameEntity> _matcherBubblesReadyToMerge;

    public static Entitas.IMatcher<GameEntity> BubblesReadyToMerge {
        get {
            if (_matcherBubblesReadyToMerge == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubblesReadyToMerge);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubblesReadyToMerge = matcher;
            }

            return _matcherBubblesReadyToMerge;
        }
    }
}
