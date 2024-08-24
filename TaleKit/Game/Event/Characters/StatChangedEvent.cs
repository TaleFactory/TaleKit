using TaleKit.Network.Packet;

namespace TaleKit.Game.Event.Characters;

public class StatChangedEvent : IEvent
{
    public Session Session { get; init; }
}