using TaleKit.Extension;
using TaleKit.Game.Maps;
using TaleKit.Game;
using TaleKit.Game.Event.Maps;
using TaleKit.Game.Factory;

namespace TaleKit.Network.Packet.Maps;

public class CMap : IPacket
{
    public int MapId { get; set; }
    public bool IsDestination { get; set; }
}

public class CMapBuilder : PacketBuilder<CMap>
{
    public override string Header { get; } = "c_map";
    
    protected override CMap CreatePacket(string[] body)
    {
        return new CMap
        {
            MapId = body[1].ToInt(),
            IsDestination = body[2].ToBool()
        };
    }
}

public class CMapProcessor : PacketProcessor<CMap>
{
    protected override void Process(Session session, CMap packet)
    {
        var map = session.Character.Map;
        
        if (!packet.IsDestination)
        {
            return;
        }

        session.Character.Map = MapFactory.CreateMap(packet.MapId);
        session.Character.Map.AddEntity(session.Character);
        
        session.Emit(new MapChangedEvent
        {
            Session = session,
            To = session.Character.Map,
            From = map
        });
    }
}