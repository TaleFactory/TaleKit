using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Social;

namespace TaleKit.Network.Packet.Interaction;

public class Say : IPacket
{
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
    public string Message { get; set; }
}

public class SayBuilder : PacketBuilder<Say>
{
    public override string Header => "say";

    protected override Say CreatePacket(string[] body)
    {
        var message = string.Join(" ", body[3..]);

        return new Say
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = int.Parse(body[1]),
            Message = message
        };
    }
}

public class SayProcessor : PacketProcessor<Say>
{
    protected override void Process(Session session, Say packet)
    {
        if (packet.EntityType != EntityType.Player)
        {
            return;
        }
        
        var map = session.Character.Map;
        if (map is null)
            return;
        
        var player = session.Character.Map.GetEntity<Player>(packet.EntityType, packet.EntityId);
        if (player == null)
        {
            return;
        }
        
        session.Emit(new PlayerSpeakEvent
        {
            Session = session,
            Player = player,
            Message = packet.Message
        });
    }
}