using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Entities;

public class EntityMovedEvent : IEvent
{
    public Session Session { get; init; }
    public LivingEntity Entity { get; init; }
    public Position From { get; init; }
    public Position To { get; init; }
}