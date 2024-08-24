using TaleKit.Game.Maps;

namespace TaleKit.Game.Event.Timespaces;

public class TimespacePopupEvent : IEvent
{
    public Session Session { get; init; }
    public Timespace Timespace { get; init; }

    public void Join()
    {
        Session.SendPacket("wreq 1");
    }
}