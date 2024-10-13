using TaleKit.Game.Maps;

namespace TaleKit.Game.Factory;

public class TimespaceFactory
{
    public static Timespace CreateTimespace(int id, int level, int x, int y)
    {
        return new Timespace
        {
            Id = id,
            Position = new Position(x, y),
        };
    }
}