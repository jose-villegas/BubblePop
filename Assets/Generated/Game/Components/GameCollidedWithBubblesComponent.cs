//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity collidedWithBubblesEntity { get { return GetGroup(GameMatcher.CollidedWithBubbles).GetSingleEntity(); } }
    public CollidedWithBubblesComponent collidedWithBubbles { get { return collidedWithBubblesEntity.collidedWithBubbles; } }
    public bool hasCollidedWithBubbles { get { return collidedWithBubblesEntity != null; } }

    public GameEntity SetCollidedWithBubbles(GameEntity[] newValue) {
        if (hasCollidedWithBubbles) {
            throw new Entitas.EntitasException("Could not set CollidedWithBubbles!\n" + this + " already has an entity with CollidedWithBubblesComponent!",
                "You should check if the context already has a collidedWithBubblesEntity before setting it or use context.ReplaceCollidedWithBubbles().");
        }
        var entity = CreateEntity();
        entity.AddCollidedWithBubbles(newValue);
        return entity;
    }

    public void ReplaceCollidedWithBubbles(GameEntity[] newValue) {
        var entity = collidedWithBubblesEntity;
        if (entity == null) {
            entity = SetCollidedWithBubbles(newValue);
        } else {
            entity.ReplaceCollidedWithBubbles(newValue);
        }
    }

    public void RemoveCollidedWithBubbles() {
        collidedWithBubblesEntity.Destroy();
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

    public CollidedWithBubblesComponent collidedWithBubbles { get { return (CollidedWithBubblesComponent)GetComponent(GameComponentsLookup.CollidedWithBubbles); } }
    public bool hasCollidedWithBubbles { get { return HasComponent(GameComponentsLookup.CollidedWithBubbles); } }

    public void AddCollidedWithBubbles(GameEntity[] newValue) {
        var index = GameComponentsLookup.CollidedWithBubbles;
        var component = (CollidedWithBubblesComponent)CreateComponent(index, typeof(CollidedWithBubblesComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCollidedWithBubbles(GameEntity[] newValue) {
        var index = GameComponentsLookup.CollidedWithBubbles;
        var component = (CollidedWithBubblesComponent)CreateComponent(index, typeof(CollidedWithBubblesComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCollidedWithBubbles() {
        RemoveComponent(GameComponentsLookup.CollidedWithBubbles);
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

    static Entitas.IMatcher<GameEntity> _matcherCollidedWithBubbles;

    public static Entitas.IMatcher<GameEntity> CollidedWithBubbles {
        get {
            if (_matcherCollidedWithBubbles == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CollidedWithBubbles);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCollidedWithBubbles = matcher;
            }

            return _matcherCollidedWithBubbles;
        }
    }
}