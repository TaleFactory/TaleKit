using TaleKit.Game.Maps;

namespace TaleKit.Game.Factory;

public static class PortalFactory
{
    public static Portal CreatePortal(int id, int destinationId, PortalType type, int x, int y)
    {
        return new Portal
        {
            Id = id,
            DestinationId = destinationId,
            Destination = MapFactory.GetMapName(destinationId) ?? "Undefined",
            PortalType = type,
            Position = new Position(x, y)
        };
    }
}