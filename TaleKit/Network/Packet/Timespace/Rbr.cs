using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Timespaces;

namespace TaleKit.Network.Packet.Timespace;

public class Rbr : IPacket
{
    public int TimespaceId { get; init; }
}

public class RbrBuilder : PacketBuilder<Rbr>
{
    public override string Header { get; } = "rbr";
    
    protected override Rbr CreatePacket(string[] body)
    {
        return new Rbr
        {
            TimespaceId = body[0].Split('.')[0].ToInt()
        };
    }
}

public class RbrProcessor : PacketProcessor<Rbr>
{
    protected override void Process(Session session, Rbr packet)
    {
        session.Emit(new TimespacePopupEvent
        {
            Timespace = session.Character.Map.GetTimespace(packet.TimespaceId),
            Session = session
        });
    }
}