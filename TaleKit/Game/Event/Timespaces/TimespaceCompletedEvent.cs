namespace TaleKit.Game.Event.Timespaces;

public class TimespaceCompletedEvent : IEvent
{
    public Session Session { get; init; }
}