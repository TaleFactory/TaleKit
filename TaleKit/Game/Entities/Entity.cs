using TaleKit.Game.Maps;

namespace TaleKit.Game.Entities;

public enum EntityType
{
    Player = 1,
    Npc = 2,
    Monster = 3,
    Drop = 9
}

public abstract class Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Position Position { get; set; }
    public Map Map { get; set; }
    
    public abstract EntityType EntityType { get; }
}