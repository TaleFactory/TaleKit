using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Glacernon;

public enum RaidKind
{
    Morcos,
    Hatus,
    Calvina,
    Berios
}

public class GlacernonRaidOpenedEvent : IEvent
{
    public required Session Session { get; init; }
    public required int TimeLeft { get; init; }
    public required GlacernonSide Side { get; init; }
    public required RaidKind RaidKind { get; init; }
}