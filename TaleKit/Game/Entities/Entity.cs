using TaleKit.Game.Maps;

namespace TaleKit.Game.Entities;

public enum EntityType
{
    Player = 1,
    Npc = 2,
    Monster = 3,
    Drop = 9
}

public abstract class Entity : IEquatable<Entity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Position Position { get; set; }
    public Map Map { get; set; }
    
    public abstract EntityType EntityType { get; }

    public bool Equals(Entity other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && EntityType == other.EntityType;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Entity)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, (int)EntityType);
    }
}