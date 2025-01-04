namespace TaleKit.Game.Event.Glacernon;

public class GlacernonPercentChangedEvent : IEvent
{
    public required Session Session { get; init; }
    public required int AngelPercent { get; init; }
    public required int DemonPercent { get; init; }
}