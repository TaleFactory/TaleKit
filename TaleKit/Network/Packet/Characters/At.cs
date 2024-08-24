using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Characters;

public class At : IPacket
{
    public int CharacterId { get; set; }
    public int MapId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
}

public class AtBuilder : PacketBuilder<At>
{
    public override string Header => "at";

    protected override At CreatePacket(string[] body)
    {
        return new At
        {
            CharacterId = body[0].ToInt(),
            MapId = body[1].ToInt(),
            PositionX = body[2].ToInt(),
            PositionY = body[3].ToInt()
        };
    }
}

public class AtProcessor : PacketProcessor<At>
{
    protected override void Process(Session session, At packet)
    {
        session.Character.Position = new Position
        {
            X = packet.PositionX,
            Y = packet.PositionY
        };
    }
}