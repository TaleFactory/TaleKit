using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Glacernon;

namespace TaleKit.Network.Packet.Glacernon;


public class Fc : IPacket
{
    public int AngelPercent { get; set; }
    public int DemonPercent { get; set; }
    
    public int AngelEvent { get; set; }
    public int DemonEvent { get; set; }
    
    public int AngelTime { get; set; }
    public int DemonTime { get; set; }
    
    public bool IsHatus { get; set; }
    public bool IsMorcos { get; set; }
    public bool IsCalvina { get; set; }
    public bool IsBerios { get; set; }
}

public class FcBuilder : PacketBuilder<Fc>
{
    public override string Header => "fc";

    protected override Fc CreatePacket(string[] body)
    {
        return new Fc
        {
            AngelPercent = body[2].ToInt(),
            AngelEvent = body[3].ToInt(),
            AngelTime = body[4].ToInt(),
            DemonPercent = body[11].ToInt(),
            DemonEvent = body[12].ToInt(),
            DemonTime = body[13].ToInt()
        };
    }
}

public class FcProcessor : PacketProcessor<Fc>
{
    protected override void Process(Session session, Fc packet)
    {
        if (packet.AngelEvent == 0 && packet.DemonEvent == 0)
        {
            session.Emit(new GlacernonPercentChangedEvent
            {
                Session = session,
                AngelPercent = packet.AngelPercent,
                DemonPercent = packet.DemonPercent
            });
        }
        
        if (packet.AngelEvent == 1 || packet.DemonEvent == 1)
        {
            session.Emit(new GlacernonMukrajuSpawnedEvent
            {
                Session = session,
                Side = packet.AngelEvent == 2 ? GlacernonSide.Angel : GlacernonSide.Demon,
                TimeLeftInSeconds = packet.AngelEvent == 2 ? packet.AngelTime : packet.DemonTime
            });
        }
        
        if (packet.AngelEvent == 3 || packet.DemonEvent == 3)
        {
            session.Emit(new GlacernonRaidOpenedEvent
            {
                Session = session,
                Side = packet.AngelEvent == 3 ? GlacernonSide.Angel : GlacernonSide.Demon,
                TimeLeft = packet.AngelEvent == 3 ? packet.AngelTime : packet.DemonTime,
            });
        }
    }
}
