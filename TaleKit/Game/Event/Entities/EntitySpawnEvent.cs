using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Entities;

public class EntitySpawnEvent : IEvent
{
    public Session Session { get; init; }
    public Entity Entity { get; init; }
}