using TaleKit.Extension;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Animation;

public class Guri : IPacket
{
    public int Id { get; init; }
    public EntityType EntityType { get; init; }
    public int EntityId { get; init; }
    public int AnimationId { get; init; }
}

public class GuriBuilder : PacketBuilder<Guri>
{
    public override string Header => "guri";

    protected override Guri CreatePacket(string[] body)
    {
        return new Guri
        {
            Id = body.Length >= 1 ? body[0].ToInt() : 0,
            EntityType = body.Length >= 2 ? body[1].ToEnum<EntityType>() : 0,
            EntityId =  body.Length >= 3 ? body[2].ToInt() : 0,
            AnimationId = body.Length >= 4 ? body[3].ToInt() : 0
        };
    }
}