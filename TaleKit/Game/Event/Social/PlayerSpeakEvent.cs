using TaleKit.Game.Entities;

namespace TaleKit.Game.Event.Social;

public class PlayerSpeakEvent : IEvent
{
    public required Session Session { get; init; }
    public required Player Player { get; init; }
    public required string Message { get; init; }
}