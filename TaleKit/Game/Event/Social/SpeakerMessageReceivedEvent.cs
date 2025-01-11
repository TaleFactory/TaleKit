namespace TaleKit.Game.Event.Social;

public class SpeakerMessageReceivedEvent : IEvent
{
    public required Session Session { get; init; }
    
    public required string Message { get; init; }
    public required string Sender { get; init; }
}