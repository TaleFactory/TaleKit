namespace TaleKit.Game.Maps;

public enum PortalType : sbyte
{
    Map = -1,
    TimespaceStart = 0,
    Closed = 1,
    Open = 2,
    Miniland = 3,
    TimeSpaceEnd = 4,
    TimeSpaceClosed = 5,
    Exit = 6,
    ExitClose = 7,
    Raid = 8,
    Effect = 9,
    BlueRaid = 10,
    DarkRaid = 11,
    TimeSpace = 12,
    Shop = 20
}

public class Portal
{
    public int Id { get; init; }
    public int DestinationId { get; init; }
    public string Destination { get; init; }
    public Position Position { get; init; }
    public PortalType PortalType { get; init; }
}