namespace TaleKit.Game.Event;

public interface IEvent
{
    public Session Session { get; init; }
}