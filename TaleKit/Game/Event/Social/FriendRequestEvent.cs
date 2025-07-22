namespace TaleKit.Game.Event.Social;

public class FriendRequestEvent(string accept, string decline) : IEvent
{
    public string SenderName  { get; init; }
    
    public Session Session { get; init; }
    
    public void Accept()
    {
        Session.SendPacket(accept);
    }
    
    public void Decline()
    {
        Session.SendPacket(decline);
    }
}