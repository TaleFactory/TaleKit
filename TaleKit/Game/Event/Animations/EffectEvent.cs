using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Animations;

public class EffectEvent : IEvent
{
    public Session Session { get; init; }
    public Entity Entity { get; init; }
    public int EffectId { get; init; }
}