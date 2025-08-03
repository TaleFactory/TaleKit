namespace TaleKit.Game.Entities;

public class Nosmate : Npc
{
    public int OwnerId { get; set; }
    public Player Owner => Map.GetEntity<Player>(EntityType.Player, OwnerId);
}