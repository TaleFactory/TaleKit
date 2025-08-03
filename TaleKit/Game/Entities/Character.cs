using TaleKit.Extension;
using TaleKit.Game.Combat;
using TaleKit.Game.Interaction;
using TaleKit.Game.Storage;
using TaleKit.Network;

namespace TaleKit.Game.Entities;

public interface IActionBridge
{
    void Walk(Character character, Position position);
    void WalkNosmate(SummonedNosmate nosmate, Position position);
    void Attack(LivingEntity entity);
    void Attack(LivingEntity entity, Skill skill);
    void PickUp(Character character, Drop drop);
}

public class Character : Player
{
    public int Health { get; set; }
    public int MaximumHealth { get; set; }
    public int Mana { get; set; }
    public int MaximumMana { get; set; }

    public HashSet<Skill> Skills { get; set; } = new();
    public HashSet<Nosmate> Nosmates { get; set; } = new();
    
    public SummonedNosmate Nosmate { get; set; }

    public Social Social { get; }
    public Inventory Inventory { get; }
    public Equipment Equipment { get; }

    public override int HpPercentage => (byte)(Health == 0 ? 0 : (double)Health / MaximumHealth * 100);
    public override int MpPercentage => (byte)(Mana == 0 ? 0 : (double)Mana / MaximumMana * 100);

    public Trade Trade { get; set; }
    
    private readonly INetwork network;
    private readonly IActionBridge bridge;

    public Character(IActionBridge bridge, INetwork network)
    {
        this.bridge = bridge;
        this.network = network;

        Social = new Social(this);
        Inventory = new Inventory(this);
        Equipment = new Equipment(this);
    }
    
    public override EntityType EntityType => EntityType.Player;
    
    public void Walk(Position destination)
    {
        bridge.Walk(this, destination);
    }
    
    public void SendTradeRequest(Player player)
    {
        network.SendPacket($"req_exc 1 {player.Id}");
    }

    public void Dance(int? optionalId = null)
    {
        var packet = "guri 2";
        if (optionalId.HasValue)
        {
            packet += $" {optionalId}";
        }
        
        network.SendPacket(packet);
    }

    public void Attack(LivingEntity entity)
    {
        var skill = Skills.FirstOrDefault();
        if (skill is null)
        {
            return;
        }

        var canAttack = Position.IsInRange(entity.Position, skill.Range) && !skill.IsOnCooldown;
        if (!canAttack)
        {
            return;
        }
        
        bridge.Attack(entity, skill);
    }

    public void Attack(LivingEntity entity, Skill skill)
    {
        var canAttack = Position.IsInRange(entity.Position, skill.Range) && !skill.IsOnCooldown;
        if (!canAttack)
        {
            return;
        }
        
        bridge.Attack(entity, skill);
    }
    
    public void AttackAt(Skill skill, Position target)
    {
        var canAttack = Position.IsInRange(target, skill.Range) && !skill.IsOnCooldown && skill.TargetType == TargetType.NoTarget;
        if (!canAttack)
        {
            return;
        }

        network.SendPacket($"u_as {skill.CastId} {target.X} {target.Y}");
    }

    public void AttackSelf(Skill skill)
    {
        var canAttack = skill.TargetType == TargetType.Self && !skill.IsOnCooldown;
        if (!canAttack)
        {
            return;
        }

        bridge.Attack(this, skill);
    }

    public void PickUp(Drop drop)
    {
        var inRange = Position.IsInRange(drop.Position, 1);
        if (!inRange)
        {
            return;
        }
        
        bridge.PickUp(this, drop);
    }
    
    internal INetwork GetNetwork() => network;
    internal IActionBridge GetBridge() => bridge;
}