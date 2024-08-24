namespace TaleKit.Game.Event.Characters;

public class GoldChangedEvent : IEvent
{
    public int From { get; init; }
    public int To { get; init; }
    
    public Session Session { get; init; }
}