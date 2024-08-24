using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Combat;

public class EntityDieEvent : IEvent
{
    public Session Session { get; init; }
    public LivingEntity Killer { get; init; }
    public LivingEntity Entity { get; init; }
}