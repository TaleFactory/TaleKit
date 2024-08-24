using TaleKit.Network.Packet.Characters;

namespace TaleKit.Game.Combat;

public enum SkillType
{
    Passive = 0,
    Player = 1,
    Upgrade = 2,
    Emote = 3,
    Monster = 4,
    Partner = 5
}

public enum HitType
{
    Target,
    EnemiesInZone,
    AlliesInZone,
    SpecialArea
}

public enum TargetType
{
    Target = 0,
    Self = 1,
    SelfOrTarget = 2,
    NoTarget = 3
}

public class Skill : IEquatable<Skill>
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
    
    public bool Equals(Skill other)
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
        return Equals((Skill)obj);
    }

    public override int GetHashCode()
    {
        return VirtualNumber;
    }
}