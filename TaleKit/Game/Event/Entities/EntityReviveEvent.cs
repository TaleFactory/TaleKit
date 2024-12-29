using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Entities;

public class EntityReviveEvent : IEvent
{
    public Session Session { get; init; }
    public LivingEntity Entity { get; init; }
}