namespace TaleKit.Game.Storage;

public enum EquipmentSlot
{
    RightHand = 0,
    Armor = 1,
    Hat = 2,
    Gloves = 3,
    Shoes = 4,
    LeftHand = 5,
    Necklace = 6,
    Ring = 7,
    Bracelet = 8,
    Accessory = 9,
    Fairy = 10,
    Amulet = 11,
    Specialist = 12,
    Costume = 13,
    CostumeHat = 14,
    CostumeWeapon = 15,
    CostumeWing = 16,
    MiniPet = 17
}

public class EquipmentItem
{
    public EquipmentSlot Slot { get; init; }
    public int VirtualNumber { get; init; }
    public string Name { get; init; }
}