namespace TaleKit.Game.Entities;

public class Monster : LivingEntity
{
    public override EntityType EntityType => EntityType.Monster;
    
    public int VirtualNumber { get; init; }
}