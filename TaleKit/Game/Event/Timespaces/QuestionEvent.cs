namespace TaleKit.Game.Event.Timespaces;

public class QuestionEvent : IEvent
{
    public Session Session { get; init; }
    
    public string Accept { get; init; }
    public string Decline { get; init; }

    public void AcceptIt()
    {
        Session.SendPacket(Accept);
    }

    public void DeclineIt()
    {
        Session.SendPacket(Decline);
    }
}