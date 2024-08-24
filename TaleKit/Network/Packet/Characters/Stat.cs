using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Event.Characters;

namespace TaleKit.Network.Packet.Characters;

public class Stat : IPacket
{
    public int Health { get; init; }
    public int MaximumHealth { get; init; }
    public int Mana { get; init; }
    public int MaximumMana { get; init; }
}

public class StatBuilder : PacketBuilder<Stat>
{
    public override string Header { get; } = "stat";
    
    protected override Stat CreatePacket(string[] body)
    {
        return new Stat
        {
            Health = body[0].ToInt(),
            MaximumHealth = body[1].ToInt(),
            Mana = body[2].ToInt(),
            MaximumMana = body[3].ToInt()
        };
    }
}

public class StatProcessor : PacketProcessor<Stat>
{
    protected override void Process(Session session, Stat packet)
    {
        session.Character.Health = packet.Health;
        session.Character.MaximumHealth = packet.MaximumHealth;
        session.Character.Mana = packet.Mana;
        session.Character.MaximumMana = packet.MaximumMana;
        
        session.Emit(new StatChangedEvent
        {
            Session = session
        });
    }
}