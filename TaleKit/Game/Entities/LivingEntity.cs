using TaleKit.Game.Combat;

namespace TaleKit.Game.Entities;

public abstract class LivingEntity : Entity
{
    public virtual int HpPercentage { get; set; }
    public virtual int MpPercentage { get; set; }
    
    public int Hp { get; set; }
    public int HpMaximum { get; set; }
        
    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }
    
    public int Speed { get; set; }

    public HashSet<Buff> Buffs { get; set; } = new();
}