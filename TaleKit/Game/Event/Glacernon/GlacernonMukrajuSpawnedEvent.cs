using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Glacernon;

public class GlacernonMukrajuSpawnedEvent : IEvent
{
    public required Session Session { get; init; }
    public required int TimeLeftInSeconds { get; init; }
    public required GlacernonSide Side { get; init; }
}