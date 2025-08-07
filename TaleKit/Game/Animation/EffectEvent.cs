using TaleKit.Game.Entities;
using TaleKit.Game.Event;

namespace TaleKit.Game.Animation;

public class EffectEvent : IEvent
{
    public Session Session { get; init; }
    public Entity Entity { get; init; }
    public int EffectId { get; init; }
}