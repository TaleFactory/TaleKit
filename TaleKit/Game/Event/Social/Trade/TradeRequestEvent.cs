using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Social.Trade;

public class TradeRequestEvent(string accept, string decline) : IEvent
{
    public required Session Session { get; init; }
    public required Player Sender { get; init; }
    
    public void Accept()
    {
        Session.SendPacket(accept);
    }
    
    public void Decline()
    {
        Session.SendPacket(decline);
    }
}