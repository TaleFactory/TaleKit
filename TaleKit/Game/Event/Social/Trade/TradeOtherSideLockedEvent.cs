namespace TaleKit.Game.Event.Social.Trade;

public class TradeOtherSideLockedEvent : IEvent
{
    public Session Session { get; init; }
    public Interaction.Trade Trade { get; init; }
}