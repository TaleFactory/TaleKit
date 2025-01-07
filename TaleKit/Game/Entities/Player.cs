namespace TaleKit.Game.Entities;

public enum GlacernonSide
{
    Angel,
    Demon
}

public class Player : LivingEntity
{
    public override EntityType EntityType => EntityType.Player;
    public GlacernonSide GlacernonSide { get; set; }
    public string FamilyName { get; set; }
}