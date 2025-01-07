namespace TaleKit.Game.Event.Social.Trade;

public class TradeCanceledEvent : IEvent
{
    public Session Session { get; init; }
}