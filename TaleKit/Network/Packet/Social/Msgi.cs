using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Social;

namespace TaleKit.Network.Packet.Social;

public class Msgi : IPacket
{
    public int MessageId { get; init; }
    public int MessageKind { get; init; }
    public int FirstParameter { get; init; }
}

public class MsgiPacket : PacketBuilder<Msgi>
{
    public override string Header => "msgi";

    protected override Msgi CreatePacket(string[] body)
    {
        return new Msgi
        {
            MessageId = body[1].ToInt(),
            MessageKind = body[2].ToInt(),
            FirstParameter = body[3].ToInt()
        };
    }
}

public class MsgiProcessor : PacketProcessor<Msgi>
{
    protected override void Process(Session session, Msgi packet)
    {
        session.Emit(new MessageReceivedEvent
        {
            MessageId = packet.MessageId,
            FirstParameter = packet.FirstParameter,
            Session = session
        });
    }
}