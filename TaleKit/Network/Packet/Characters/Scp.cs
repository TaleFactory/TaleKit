using System.Transactions;
using TaleKit.Extension;
using TaleKit.Game.Factory;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Registry;

namespace TaleKit.Network.Packet.Characters;

public class Scp : IPacket
{
    public int Index { get; init; }
    public int VirtualNumber { get; init; }
    public int EntityId { get; init; }
    public int Level { get; init; }
    public int Stars { get; init; }
    public int HeroLevel { get; init; }
    public string Name { get; init; }
}

public class ScpBuilder : PacketBuilder<Scp>
{
    public override string Header => "sc_p";

    protected override Scp CreatePacket(string[] body)
    {
        return new Scp
        {
            Index = body[0].ToInt(),
            VirtualNumber = body[1].ToInt(),
            EntityId = body[2].ToInt(),
            Level = body[3].ToInt(),
            Name = body[31],
            Stars = body[34].ToInt(),
            HeroLevel = body[35].ToInt()
        };
    }
}

public class ScpProcessor : PacketProcessor<Scp>
{
    protected override void Process(Session session, Scp packet)
    {
        var data = MonsterRegistry.GetMonsterData(packet.VirtualNumber);
        var name= TranslationRegistry.GetTranslation(TranslationGroup.Monsters, TaleKitSettings.Language, data?.NameKey ?? string.Empty) 
                  ?? "Undefined";
        
        session.Character.Nosmates.Add(new Nosmate
        {
            Name = packet.Name == "@" ? name : packet.Name,
            Index = packet.Index,
            VirtualNumber = packet.VirtualNumber,
            HeroLevel = packet.HeroLevel,
            Stars = packet.Stars,
            Level = packet.Level
        });
    }
}