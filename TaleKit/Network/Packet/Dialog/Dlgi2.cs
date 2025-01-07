using TaleKit.Game;
using TaleKit.Game.Event.Social;
using TaleKit.Game.Event.Social.Trade;

namespace TaleKit.Network.Packet.Dialog;

public class Dlgi2 : IPacket
{
    public string Accept { get; init; }
    public string Decline { get; init; }
    
    public int Id { get; init; }
    public string Sender { get; init; }
}

public class Dlgi2Builder : PacketBuilder<Dlgi2>
{
    public override string Header { get; } = "dlgi2";

    protected override Dlgi2 CreatePacket(string[] body)
    {
        var id = int.Parse(body[2]);

        var sender = id switch
        {
            169 => body[5],
            _ => body[4]
        };

        return new Dlgi2
        {
            Accept = body[0],
            Decline = body[1],
            Id = id,
            Sender = sender
        };
    }
}

public class Dlgi2Processor : PacketProcessor<Dlgi2>
{
    protected override void Process(Session session, Dlgi2 packet)
    {
        switch (packet.Id)
        {
            case 169:
            {
                var sender = session.Character.Map.GetPlayerByName(packet.Sender);
                if (sender == null)
                {
                    return;
                }
                
                session.Emit(new TradeRequestEvent(packet.Accept, packet.Decline)
                {
                    Session = session,
                    Sender = sender
                });
                break;
            }
            case 233:
            {
                session.Emit(new GroupRequestEvent(packet.Accept, packet.Decline)
                {
                    Session = session,
                    SenderName = packet.Sender
                });
                break;
            }
        }
    }
}

