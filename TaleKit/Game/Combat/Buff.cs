namespace TaleKit.Game.Combat;

public class Buff : IEquatable<Buff>
{
    public required int VirtualNumber { get; init; }
    public required string Name { get; init; }
    public required int Duration { get; init; }

    public bool Equals(Buff other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return VirtualNumber == other.VirtualNumber;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Buff)obj);
    }

    public override int GetHashCode()
    {
        return VirtualNumber;
    }
}