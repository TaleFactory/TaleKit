using TaleKit.Extension;
using TaleKit.Game.Combat;
using TaleKit.Game.Interaction;
using TaleKit.Game.Storage;
using TaleKit.Network;

namespace TaleKit.Game.Entities;

public interface IActionBridge
{
    Session Session { get; set; }
    
    void Walk(Position position, int speed);
    void Attack(LivingEntity entity);
    void Attack(LivingEntity entity, Skill skill);
    void PickUp(Drop drop);
}

public class Character : Player
{
    public int Health { get; set; }
    public int MaximumHealth { get; set; }
    public int Mana { get; set; }
    public int MaximumMana { get; set; }

    public HashSet<Skill> Skills { get; set; } = new();
    public HashSet<Nosmate> Nosmates { get; set; } = new();

    public Social Social { get; }
    public Inventory Inventory { get; }
    public Equipment Equipment { get; }

    public override int HpPercentage => (byte)(Health == 0 ? 0 : (double)Health / MaximumHealth * 100);
    public override int MpPercentage => (byte)(Mana == 0 ? 0 : (double)Mana / MaximumMana * 100);

    private readonly INetwork network;
    private readonly IActionBridge bridge;

    private CancellationTokenSource currentTaskCts;

    public Character(IActionBridge bridge, INetwork network)
    {
        this.bridge = bridge;
        this.network = network;

        Social = new Social(network);
        Inventory = new Inventory(this, network);
        Equipment = new Equipment(network);
    }
    
    public override EntityType EntityType => EntityType.Player;

    public void Walk(Position destination)
    {
        if (currentTaskCts is not null)
        {
            currentTaskCts.Cancel();
        }

        currentTaskCts = new CancellationTokenSource();
        
        if (!Map.IsWalkable(destination))
        {
            return;
        }

        _ = Walk(destination, currentTaskCts.Token);
    }

    public async Task Dance(int id, int time)
    {
        const int sleep = 1000;
        
        var count = time / sleep;
        
        network.SendPacket("guri 2");
        for (var i = 0; i < count; i++)
        {
            network.SendPacket($"guri 5 1 {Id} {i * 20} {id}");
            await Task.Delay(sleep);
        }
    }
    
    private async Task Walk(Position destination, CancellationToken cancellationToken)
    {
        var positiveX = destination.X > Position.X;
        var positiveY = destination.Y > Position.Y;
        
        while(!Position.Equals(destination) && !cancellationToken.IsCancellationRequested)
        {
            var distance = Position.Distance(destination);

            var stepX = distance.X >= 5 ? 5 : distance.X;
            var stepY = distance.Y >= 5 ? 5 : distance.Y;
            
            var stepTotal = stepX + stepY;
            if (stepTotal > 5)
            {
                var difference = stepTotal - 5;
                var split = difference / 2 + (difference % 2);

                stepX -= split;
                stepY -= split;
            }
            
            var x = (positiveX ? 1 : -1) * stepX + Position.X;
            var y = (positiveY ? 1 : -1) * stepY + Position.Y;

            var target = new Position(x, y);

            if (!Map.IsWalkable(target))
            {
                break;
            }
            
            bridge.Walk(target, Speed);

            try
            {
                await Task.Delay((stepX + stepY) * (2000 / Speed), cancellationToken);
            }
            catch (TaskCanceledException) { }
            finally
            {
                Position = target;
            }
        }
    }

    public void Attack(LivingEntity entity)
    {
        var skill = Skills.FirstOrDefault();
        if (skill is null)
        {
            return;
        }

        var canAttack = Position.IsInRange(entity.Position, skill.Range);
        if (!canAttack)
        {
            return;
        }
        
        bridge.Attack(entity, skill);
    }

    public void Attack(LivingEntity entity, Skill skill)
    {
        var canAttack = Position.IsInRange(entity.Position, skill.Range);
        if (!canAttack)
        {
            return;
        }
        
        bridge.Attack(entity, skill);
    }

    public void PickUp(Drop drop)
    {
        var inRange = Position.IsInRange(drop.Position, 1);
        if (!inRange)
        {
            return;
        }
        
        bridge.PickUp(drop);
    }
}