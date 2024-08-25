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