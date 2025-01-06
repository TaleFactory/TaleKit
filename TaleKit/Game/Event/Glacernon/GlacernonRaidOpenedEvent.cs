using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Glacernon;


public class GlacernonRaidOpenedEvent : IEvent
{
    public required Session Session { get; init; }
    public required int TimeLeft { get; init; }
    public required GlacernonSide Side { get; init; }
}