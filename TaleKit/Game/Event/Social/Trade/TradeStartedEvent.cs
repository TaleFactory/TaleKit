namespace TaleKit.Game.Event.Social.Trade;

public class TradeStartedEvent : IEvent
{
    public Session Session { get; init; }
    public Interaction.Trade Trade { get; init; }
}