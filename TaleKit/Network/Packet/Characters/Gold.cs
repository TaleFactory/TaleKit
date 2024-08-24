using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Characters;

namespace TaleKit.Network.Packet.Characters;

public class Gold : IPacket
{
    public int Amount { get; init; }
}

public class GoldBuilder : PacketBuilder<Gold>
{
    public override string Header { get; } = "gold";
    
    protected override Gold CreatePacket(string[] body)
    {
        return new Gold
        {
            Amount = body[0].ToInt()
        };
    }
}

public class GoldProcessor : PacketProcessor<Gold>
{
    protected override void Process(Session session, Gold packet)
    {
        var currentGold = session.Character.Inventory.Gold;
        
        session.Character.Inventory.Gold = packet.Amount;
        
        session.Emit(new GoldChangedEvent
        {
            From = currentGold,
            To = session.Character.Inventory.Gold,
            Session = session
        });
    }
}