using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Entities;

public class EntityTeleportedEvent : IEvent
{
    public Session Session { get; init; }
    public Entity Entity { get; init; }
    public Position At { get; init; }
}