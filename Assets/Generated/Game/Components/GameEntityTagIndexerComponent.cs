//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity entityTagIndexerEntity { get { return GetGroup(GameMatcher.EntityTagIndexer).GetSingleEntity(); } }
    public EntityTagIndexer entityTagIndexer { get { return entityTagIndexerEntity.entityTagIndexer; } }
    public bool hasEntityTagIndexer { get { return entityTagIndexerEntity != null; } }

    public GameEntity SetEntityTagIndexer(System.Collections.Generic.Dictionary<string, Entitas.IEntity> newValue) {
        if (hasEntityTagIndexer) {
            throw new Entitas.EntitasException("Could not set EntityTagIndexer!\n" + this + " already has an entity with EntityTagIndexer!",
                "You should check if the context already has a entityTagIndexerEntity before setting it or use context.ReplaceEntityTagIndexer().");
        }
        var entity = CreateEntity();
        entity.AddEntityTagIndexer(newValue);
        return entity;
    }

    public void ReplaceEntityTagIndexer(System.Collections.Generic.Dictionary<string, Entitas.IEntity> newValue) {
        var entity = entityTagIndexerEntity;
        if (entity == null) {
            entity = SetEntityTagIndexer(newValue);
        } else {
            entity.ReplaceEntityTagIndexer(newValue);
        }
    }

    public void RemoveEntityTagIndexer() {
        entityTagIndexerEntity.Destroy();
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

    public EntityTagIndexer entityTagIndexer { get { return (EntityTagIndexer)GetComponent(GameComponentsLookup.EntityTagIndexer); } }
    public bool hasEntityTagIndexer { get { return HasComponent(GameComponentsLookup.EntityTagIndexer); } }

    public void AddEntityTagIndexer(System.Collections.Generic.Dictionary<string, Entitas.IEntity> newValue) {
        var index = GameComponentsLookup.EntityTagIndexer;
        var component = (EntityTagIndexer)CreateComponent(index, typeof(EntityTagIndexer));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceEntityTagIndexer(System.Collections.Generic.Dictionary<string, Entitas.IEntity> newValue) {
        var index = GameComponentsLookup.EntityTagIndexer;
        var component = (EntityTagIndexer)CreateComponent(index, typeof(EntityTagIndexer));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveEntityTagIndexer() {
        RemoveComponent(GameComponentsLookup.EntityTagIndexer);
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

    static Entitas.IMatcher<GameEntity> _matcherEntityTagIndexer;

    public static Entitas.IMatcher<GameEntity> EntityTagIndexer {
        get {
            if (_matcherEntityTagIndexer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EntityTagIndexer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEntityTagIndexer = matcher;
            }

            return _matcherEntityTagIndexer;
        }
    }
}
