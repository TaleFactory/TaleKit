﻿using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Animation;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Animation;

public class Eff : IPacket
{
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
    public int EffectId { get; set; }
}

public class EffBuilder : PacketBuilder<Eff>
{
    public override string Header { get; } = "eff";
    
    protected override Eff CreatePacket(string[] body)
    {
        return new Eff
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            EffectId = body[2].ToInt()
        };
    }
}

public class EffProcessor : PacketProcessor<Eff>
{
    protected override void Process(Session session, Eff packet)
    {
        var entity = session.Character.Map.GetEntity<Entity>(packet.EntityType, packet.EntityId);
        if (entity is null)
            return;
        
        session.Emit(new EffectEvent
        {
            Session = session,
            Entity = entity,
            EffectId = packet.EffectId
        });
    }
}