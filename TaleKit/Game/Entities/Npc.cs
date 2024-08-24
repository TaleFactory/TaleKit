namespace TaleKit.Game.Entities;

public class Npc : LivingEntity
{
    public override EntityType EntityType => EntityType.Npc;
    
    public int VirtualNumber { get; init; }
}