using TaleKit.Extension;
using TaleKit.Game.Factory;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Characters;

public class Scp : IPacket
{
    public int Index { get; init; }
    public int VirtualNumber { get; init; }
    public int EntityId { get; init; }
    public int Level { get; init; }
    public int HeroLevel { get; init; }
}

public class ScpBuilder : PacketBuilder<Scp>
{
    public override string Header => "sc_p";

    protected override Scp CreatePacket(string[] body)
    {
        return new Scp
        {
            Index = body[0].ToInt(),
            VirtualNumber = body[1].ToInt(),
            EntityId = body[2].ToInt(),
            Level = body[34].ToInt(),
            HeroLevel = body[35].ToInt()
        };
    }
}

public class ScpProcessor : PacketProcessor<Scp>
{
    protected override void Process(Session session, Scp packet)
    {
        session.Character.Nosmates.Add(new Nosmate
        {
            Name = "Undefined",
            Index = packet.Index,
            VirtualNumber = packet.VirtualNumber,
            HeroLevel = packet.HeroLevel,
            Level = packet.Level
        });
    }
}