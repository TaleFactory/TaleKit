namespace TaleKit.Game.Event.Social;

public class MessageReceivedEvent : IEvent
{
    public Session Session { get; init; }
    public int MessageId { get; init; }
    public int FirstParameter { get; init; }
}