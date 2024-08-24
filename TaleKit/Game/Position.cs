namespace TaleKit.Game;

public readonly struct Position : IEquatable<Position>
{
    private static readonly double Sqrt = Math.Sqrt(2);
    
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; init; }
    public int Y { get; init; }

    public Position Clone()
    {
        return new Position
        {
            X = X,
            Y = Y
        };
    }

    public Position Distance(Position position)
    {
        var x = Math.Abs(X - position.X);
        var y = Math.Abs(Y - position.Y);

        return new Position(x, y);
    }
    
    public int GetDistance(Position destination)
    {
        var x = Math.Abs(X - destination.X);
        var y = Math.Abs(Y - destination.Y);

        var min = Math.Min(x, y);
        var max = Math.Max(x, y);

        return (int)(min * Sqrt + max - min);
    }
    
    public bool IsInRange(Position position, int range)
    {
        var dx = Math.Abs(X - position.X);
        var dy = Math.Abs(Y - position.Y);
        return dx <= range && dy <= range && dx + dy <= range + range / 2;
    }

    public bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        return obj is Position other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{X}/{Y}";
    }

    public static bool operator ==(Position left, Position right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Position left, Position right)
    {
        return !(left == right);
    }
}