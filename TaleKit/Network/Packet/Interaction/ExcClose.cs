using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Social;
using TaleKit.Game.Event.Social.Trade;

namespace TaleKit.Network.Packet.Interaction;

public enum TradeCloseType
{
    Cancel = 0,
    Completed = 1
}

public class ExcClose : IPacket
{
    public TradeCloseType Type { get; init; }
}

public class ExcCloseBuilder : PacketBuilder<ExcClose>
{
    public override string Header { get; } = "exc_close";

    protected override ExcClose CreatePacket(string[] body)
    {
        return new ExcClose
        {
            Type = body[0].ToEnum<TradeCloseType>()
        };
    }
}

public class ExcCloseProcessor : PacketProcessor<ExcClose>
{
    protected override void Process(Session session, ExcClose packet)
    {
        var trade = session.Character.Trade;

        switch (packet.Type)
        {
            case TradeCloseType.Cancel:
                session.Emit(new TradeCanceledEvent
                {
                    Session = session
                });
                break;
            case TradeCloseType.Completed:
                session.Emit(new TradeCompletedEvent
                {
                    Session = session,
                    Trade = trade
                });
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        session.Character.Trade = null;
    }
}