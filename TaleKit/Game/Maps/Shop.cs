using TaleKit.Game.Entities;

namespace TaleKit.Game.Maps;

public class Shop
{
    public int Id => Owner.Id;
    public Map Map => Owner.Map;
    public Position Position => Owner.Position;
    
    public required Npc Owner { get; init; }
    public required string Name { get; init; }
}