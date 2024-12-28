using TaleKit.Game.Combat;
using TaleKit.Network;

namespace TaleKit.Game.Entities;

public class Nosmate : IEquatable<Nosmate>
{
    public int Id { get; init; }
    public int Index { get; init; }
    public int VirtualNumber { get; init; }
    public string Name { get; init; }
    public int Level { get; init; }
    public int HeroLevel { get; init; }
    public int Stars { get; init; }

    public bool Equals(Nosmate other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Nosmate)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

public class NosmateSkill : IEquatable<NosmateSkill>
{
    public int Id { get; init; }

    public bool Equals(NosmateSkill other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((NosmateSkill)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

public class SummonedNosmate
{
    public required Nosmate Nosmate { get; init; }
    public List<NosmateSkill> Skills { get; set; } = new();
    
    public Character Owner { get; }
    public LivingEntity Entity => Owner.Map.GetEntity<Npc>(EntityType.Npc, Nosmate.Id);

    public SummonedNosmate(Character owner)
    {
        Owner = owner;
    }
    
    public void Attack(LivingEntity target, NosmateSkill skill)
    {
        if (!Skills.Contains(skill))
        {
            return;
        }
        
        Owner.GetNetwork().SendPacket($"u_pet {Entity.Id} {(int)target.EntityType} {target.Id} {skill.Id} {Entity.Position.X} {Entity.Position.Y}");
    }

    public void AttackSelf(NosmateSkill skill)
    {
        Attack(Entity, skill);
    }
}