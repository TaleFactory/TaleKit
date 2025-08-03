namespace TaleKit.Game.Entities;

public class Player : LivingEntity
{
    public override EntityType EntityType => EntityType.Player;
    public string FamilyName { get; set; }
    public int Level { get; set; }
    public int HeroLevel { get; set; }
    
    public List<Nosmate> Nosmates { get; set; } = [];
}