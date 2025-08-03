using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Characters;

public class PartyInit : IPacket
{
    public List<PartyMember> Members { get; init; }
}

public class PartyMember
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; init; }
    public string Name { get; init; }
    public int VirtualNumber { get; init; }
}

public class PartyInitBuilder : PacketBuilder<PartyInit>
{
    public override string Header { get; } = "pinit";
    
    protected override PartyInit CreatePacket(string[] body)
    {
        var members = new List<PartyMember>();
        foreach (var entityInfo in body.Skip(1))
        {
            var entity = entityInfo.Split('|');
            
            var member = new PartyMember
            {
                EntityType = entity[0].ToEnum<EntityType>(),
                EntityId = entity[1].ToInt(),
                Name = entity[4],
                VirtualNumber = entity[6].ToInt()
            };
            
            members.Add(member);
        }

        return new PartyInit
        {
            Members = members
        };
    }
}

public class PartyInitProcessor : PacketProcessor<PartyInit>
{
    protected override void Process(Session session, PartyInit packet)
    {

    }
}