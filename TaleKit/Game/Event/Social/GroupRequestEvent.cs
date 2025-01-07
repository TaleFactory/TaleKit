namespace TaleKit.Game.Event.Social;

public class GroupRequestEvent(string accept, string decline) : IEvent
{
    public required Session Session { get; init; }
    public required string SenderName { get; init; }
    
    public void Accept()
    {
        Session.SendPacket(accept);
    }
    
    public void Decline()
    {
        Session.SendPacket(decline);
    }
}