using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Glacernon;

namespace TaleKit.Network.Packet.Glacernon;

public class Fc : IPacket
{
    public int AngelPercent { get; set; }
    public int DemonPercent { get; set; }
}

public class FcBuilder : PacketBuilder<Fc>
{
    public override string Header => "fc";

    protected override Fc CreatePacket(string[] body)
    {
        return new Fc
        {
            AngelPercent = body[2].ToInt(),
            DemonPercent = body[11].ToInt()
        };
    }
}

public class FcProcessor : PacketProcessor<Fc>
{
    protected override void Process(Session session, Fc packet)
    {
        session.Emit(new GlacernonPercentChangedEvent
        {
            Session = session,
            AngelPercent = packet.AngelPercent,
            DemonPercent = packet.DemonPercent
        });
    }
}
