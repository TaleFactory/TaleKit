using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Social.Trade;
using TaleKit.Game.Interaction;

namespace TaleKit.Network.Packet.Interaction;

public class ExcList : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; init; }
    public int Gold { get; init; }
    public int BankGold { get; init; }
}

public class ExcListBuilder : PacketBuilder<ExcList>
{
    public override string Header => "exc_list";

    protected override ExcList CreatePacket(string[] body)
    {
        return new ExcList
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            Gold = body[2].ToInt(),
            BankGold = body[3].ToInt()
        };
    }
}

[ExecuteOnlyOn(Direction = PacketDirection.Receive)]
public class ExcListProcessor : PacketProcessor<ExcList>
{
    protected override void Process(Session session, ExcList packet)
    {
        var trade = session.Character.Trade;
        
        if (trade == null)
        {
            var sender = session.Character.Map.GetEntity<Player>(packet.EntityType, packet.EntityId);
            if (sender == null)
            {
                return;
            }

            session.Character.Trade = new Trade(session)
            {
                Other = sender
            };
            
            session.Emit(new TradeStartedEvent
            {
                Session = session,
                Trade = session.Character.Trade
            });
        }
        else
        {
            trade.OtherSide = new TradeSide
            {
                Gold = packet.Gold,
                BankGold = packet.BankGold
            };
            
            session.Emit(new TradeOtherSideLockedEvent
            {
                Session = session,
                Trade = trade
            });
        }
    }
}