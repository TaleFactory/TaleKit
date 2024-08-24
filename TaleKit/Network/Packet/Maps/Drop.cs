using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Factory;

namespace TaleKit.Network.Packet.Maps;

public class Drop : IPacket
{
    public int VNum { get; set; }
    public int DropId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Amount { get; set; }
    public int OwnerId { get; set; }
}

public class DropBuilder : PacketBuilder<Drop>
{
    public override string Header { get; } = "drop";
    
    protected override Drop CreatePacket(string[] body)
    {
        return new Drop
        {
            VNum = body[0].ToInt(),
            DropId = body[1].ToInt(),
            PositionX = body[2].ToInt(),
            PositionY = body[3].ToInt(),
            Amount = body[4].ToInt(),
            OwnerId = body[5].ToInt()
        };
    }
}

public class DropProcessor : PacketProcessor<Drop>
{
    protected override void Process(Session session, Drop packet)
    {
        var map = session.Character.Map;
        if (map is null)
        {
            return;
        }

        var drop = DropFactory.CreateDrop(packet.DropId, packet.VNum, packet.Amount);

        drop.Map = map;
        drop.Position = new Position(packet.PositionX, packet.PositionY);
        
        map.AddEntity(drop);
    }
}