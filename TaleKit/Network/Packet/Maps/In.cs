using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Entities;
using TaleKit.Game.Factory;

namespace TaleKit.Network.Packet.Maps;

public class In : IPacket
{
    public EntityType EntityType { get; set; }
    public string Name { get; set; }
    public int VirtualNumber { get; set; }
    public int EntityId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public InNpc Npc { get; set; }
    public InDrop Drop { get; set; }
    public InPlayer Player { get; set; }
}

public class InNpc
{
    public int HpPercentage { get; init; }
    public int MpPercentage { get; init; }
    public int Owner { get; init; }
    public string Name { get; init; }
    public int Type { get; init; }
}

public class InDrop
{
    public int Amount { get; init; }
    public bool IsQuest { get; init; }
    public int OwnerId { get; init; }
}

public class InPlayer
{
    public string Name { get; init; }
}

public class InBuilder : PacketBuilder<In>
{
    public override string Header { get; } = "in";
    
    protected override In CreatePacket(string[] body)
    {
        var entityType = body[0].ToEnum<EntityType>();
        var startIndex = entityType == EntityType.Player ? 3 : 2;
        
        var packet = new In
        {
            EntityType = entityType,
            Name = entityType == EntityType.Player ? body[1] : string.Empty,
            VirtualNumber = entityType != EntityType.Player ? body[1].ToInt() : 0,
            EntityId = body[startIndex].ToInt(),
            X = body[startIndex + 1].ToInt(),
            Y = body[startIndex + 2].ToInt()
        };

        switch (entityType)
        {
            case EntityType.Monster:
            case EntityType.Npc:
                packet.Npc = new InNpc
                {
                    HpPercentage = body[startIndex + 4].ToInt(),
                    MpPercentage = body[startIndex + 5].ToInt(),
                    Owner = body[startIndex + 9].ToInt(),
                    Name = body[startIndex + 13],
                    Type = body[startIndex + 14].ToInt()
                };
                break;
            case EntityType.Drop:
                packet.Drop = new InDrop
                {
                    Amount = body[startIndex + 3].ToInt(),
                    IsQuest = body[startIndex + 4].ToBool(),
                    OwnerId = body[startIndex + 5].ToInt()
                };
                break;
            case EntityType.Player:
                break;
        }

        return packet;
    }
}

public class InProcessor : PacketProcessor<In>
{
    protected override void Process(Session session, In packet)
    {
        var map = session.Character.Map;
        if (map is null)
        {
            return;
        }

        Entity entity = null;
        switch (packet.EntityType)
        {
            case EntityType.Monster:
                var monster = MonsterFactory.CreateMonster(packet.EntityId, packet.VirtualNumber);

                if (monster.Name.Contains("Ombre du"))
                {
                    return;
                }
                
                monster.HpPercentage = packet.Npc.HpPercentage;
                monster.MpPercentage = packet.Npc.MpPercentage;
                monster.Position = new Position(packet.X, packet.Y);
                monster.Map = map;
                
                map.AddEntity(monster);
                
                entity = monster;
                break;
            case EntityType.Drop:
                var drop = DropFactory.CreateDrop(packet.EntityId, packet.VirtualNumber, packet.Drop.Amount);

                drop.Map = map;
                drop.Position = new Position(packet.X, packet.Y);
                
                map.AddEntity(drop);

                entity = drop;
                break;
            case EntityType.Npc:
                var npc = NpcFactory.CreateNpc(packet.EntityId, packet.VirtualNumber);

                npc.HpPercentage = packet.Npc.HpPercentage;
                npc.MpPercentage = packet.Npc.MpPercentage;
                npc.Position = new Position(packet.X, packet.Y);
                npc.Map = map;
                
                map.AddEntity(npc);
                
                entity = npc;
                break;
            case EntityType.Player:
                var player = new Player
                {
                    Id = packet.EntityId,
                    Name = packet.Name,
                    Position = new Position(packet.X, packet.Y),
                    Map = map
                };
                
                map.AddEntity(player);

                entity = player;
                break;
        }

        if (entity is not null)
        {
            session.Emit(new EntitySpawnEvent
            {
                Session = session,
                Entity = entity
            });
        }
    }
}