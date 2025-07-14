using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Entities;

public class EntityLeftEvent : IEvent
{
    public Session Session { get; init; }
    public Entity Entity { get; init; }
}