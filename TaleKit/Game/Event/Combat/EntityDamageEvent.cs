using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Combat;

public class EntityDamageEvent : IEvent
{
    public Session Session { get; init; }
    public LivingEntity Caster { get; init; }
    public LivingEntity Target { get; init; }
    public int Damage { get; init; }
}