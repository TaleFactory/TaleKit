using TaleKit.Game;
using TaleKit.Game.Event.Timespaces;

namespace TaleKit.Network.Packet.Timespace;

public class Dlgi : IPacket
{
    public string Accept { get; init; }
    public string Decline { get; init; }
}

public class DlgiBuilder : PacketBuilder<Dlgi>
{
    public override string Header { get; } = "dlgi";
    
    protected override Dlgi CreatePacket(string[] body)
    {
        return new Dlgi
        {
            Accept = body[0],
            Decline = body[1]
        };
    }
}

public class DlgiProcessor : PacketProcessor<Dlgi>
{
    protected override void Process(Session session, Dlgi packet)
    {
        session.Emit(new QuestionEvent(packet.Accept, packet.Decline)
        {
            Session = session
        });
    }
}