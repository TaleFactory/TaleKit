using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Social;

namespace TaleKit.Network.Packet.Interaction;

public class Sayt : IPacket
{
    public string Message { get; init; }
    public string Sender { get; init; }
    public int Kind { get; set; }
}

public class SaytBuilder : PacketBuilder<Sayt>
{
    public override string Header { get; } = "sayt";
    
    protected override Sayt CreatePacket(string[] body)
    {
        var message = string.Join(" ", body.Skip(4).ToArray());

        return new Sayt
        {
            Kind = body[2].ToInt(),
            Sender = body[3],
            Message = message
        };
    }
}

public class SaytProcessor : PacketProcessor<Sayt>
{
    protected override void Process(Session session, Sayt packet)
    {
        if (packet.Kind == 13)
        {
            session.Emit(new SpeakerMessageReceivedEvent
            {
                Session = session,
                Sender = packet.Sender,
                Message = packet.Message
            });
        }
    }
}