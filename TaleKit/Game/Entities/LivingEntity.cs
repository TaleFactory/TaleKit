namespace TaleKit.Game.Entities;

public abstract class LivingEntity : Entity
{
    public virtual int HpPercentage { get; set; }
    public virtual int MpPercentage { get; set; }
    
        
    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }
    
    public int Speed { get; set; }
}