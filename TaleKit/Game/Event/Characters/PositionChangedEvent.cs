namespace TaleKit.Game.Event.Characters;

public class PositionChangedEvent : IEvent
{
    public Position From { get; init; }
    public Position To { get; init; }
    
    public Session Session { get; init; }
}