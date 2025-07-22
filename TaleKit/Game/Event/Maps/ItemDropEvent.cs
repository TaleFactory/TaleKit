using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Maps;

public class ItemDropEvent : IEvent
{
    public Session Session { get; init; }
    public Drop Drop { get; init; }
}