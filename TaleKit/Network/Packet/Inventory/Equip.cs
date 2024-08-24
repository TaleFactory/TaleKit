using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Factory;
using TaleKit.Game.Storage;

namespace TaleKit.Network.Packet.Inventory;

public class Equip : IPacket
{
    public required List<EquipItem> Items { get; init; }
}

public class EquipItem
{
    public int Slot { get; init; }
    public int VirtualNumber { get; init; }
}

public class EquipBuilder : PacketBuilder<Equip>
{
    public override string Header { get; } = "equip";
    
    protected override Equip CreatePacket(string[] body)
    {
        var equips = new List<EquipItem>();
        foreach (var value in body.Skip(2))
        {
            var split = value.Split('.');
            
            equips.Add(new EquipItem
            {
                Slot = split[0].ToInt(),
                VirtualNumber = split[1].ToInt()
            });
        }

        return new Equip
        {
            Items = equips
        };
    }
}

public class EquipProcessor : PacketProcessor<Equip>
{
    protected override void Process(Session session, Equip packet)
    {
        session.Character.Equipment.Clear();
        foreach (var item in packet.Items)
        {
            var equipment = ItemFactory.CreateEquipmentItem(item.VirtualNumber, (EquipmentSlot)item.Slot);
            if (equipment is not null)
            {
                session.Character.Equipment.SetItem(equipment);
            }
        }
    }
}