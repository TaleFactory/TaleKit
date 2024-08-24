namespace TaleKit.Game.Event.Timespaces;

public class QuestionEvent : IEvent
{
    public Session Session { get; init; }

    private readonly string accept;
    private readonly string decline;

    public QuestionEvent(string accept, string decline)
    {
        this.accept = accept;
        this.decline = decline;
    }

    public void Accept()
    {
        Session.SendPacket(accept);
    }

    public void Decline()
    {
        Session.SendPacket(decline);
    }
}