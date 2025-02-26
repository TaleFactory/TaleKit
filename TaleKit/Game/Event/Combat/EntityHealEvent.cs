using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Combat;

public class EntityHealEvent : IEvent
{
    public Session Session { get; init; }
    public LivingEntity Entity { get; init; }
    public int Amount { get; init; }
}