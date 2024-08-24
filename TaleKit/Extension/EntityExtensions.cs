using TaleKit.Game.Entities;

namespace TaleKit.Extension;

public static class EntityExtensions
{
    public static Monster GetClosestMonster(this Entity entity)
    {
        return entity.Map.Monsters.MinBy(x => x.Position.GetDistance(entity.Position));
    }
    
    public static Drop GetClosestDrop(this Entity entity)
    {
        return entity.Map.Drops.MinBy(x => x.Position.GetDistance(entity.Position));
    }

    public static IEnumerable<Monster> GetEnemiesInRange(this Entity entity, int range)
    {
        return entity.Map.Monsters.Where(x => x.Position.IsInRange(entity.Position, range));
    }
}