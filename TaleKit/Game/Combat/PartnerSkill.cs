namespace TaleKit.Game.Combat;

public class PartnerSkill : IEquatable<PartnerSkill>
{
    public int VirtualNumber { get; init; }
    public string Name { get; init; }
    public int Range { get; init; }
    public SkillType Type { get; init; }
    public HitType HitType { get; init; }
    public TargetType TargetType { get; init; }
    public int CastTime { get; init; }
    public int CastId { get; init; }
    public bool IsOnCooldown { get; set; }
    public int ZoneRange { get; set; }

    public bool Equals(PartnerSkill other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return VirtualNumber == other.VirtualNumber;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PartnerSkill)obj);
    }

    public override int GetHashCode()
    {
        return VirtualNumber;
    }
}