namespace TaleKit.Game.Entities;

public class Drop : Entity
{
    public override EntityType EntityType => EntityType.Drop;
    
    public int VirtualNumber { get; init; }
    public int Amount { get; init; }
}