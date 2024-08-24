using TaleKit.Game.Maps;

namespace TaleKit.Game.Event.Maps;

public class MapChangedEvent : IEvent
{
    public Session Session { get; init; }
    public Map To { get; init; }
    public Map From { get; init; }
}