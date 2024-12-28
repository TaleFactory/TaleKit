using TaleKit.Game.Combat;
using TaleKit.Network;

namespace TaleKit.Game.Entities;

public class Nosmate : IEquatable<Nosmate>
{
    public int Id { get; init; }
    public int Index { get; init; }
    public int VirtualNumber { get; init; }
    public string Name { get; init; }
    public int Level { get; init; }
    public int HeroLevel { get; init; }
    public int Stars { get; init; }

    public bool Equals(Nosmate other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Nosmate)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

public class NosmateSkill : IEquatable<NosmateSkill>
{
    public int Id { get; init; }

    public bool Equals(NosmateSkill other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((NosmateSkill)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

public class SummonedNosmate
{
    public required Nosmate Nosmate { get; init; }
    public List<NosmateSkill> Skills { get; set; } = new();
    
    public Character Owner { get; }
    public LivingEntity Entity => Owner.Map.GetEntity<Npc>(EntityType.Npc, Nosmate.Id);
    
    private CancellationTokenSource currentTaskCts;

    public SummonedNosmate(Character owner)
    {
        Owner = owner;
    }
    
    public void Walk(Position destination)
    {
        _ = WalkAsync(destination);
    }

    public Task WalkAsync(Position destination)
    {
        if (currentTaskCts is not null)
        {
            currentTaskCts.Cancel();
        }

        currentTaskCts = new CancellationTokenSource();
        
        if (!Entity.Map.IsWalkable(destination))
        {
            return Task.CompletedTask;
        }

        return Walk(destination, currentTaskCts.Token);
    }
    
    public void Attack(LivingEntity target, NosmateSkill skill)
    {
        if (!Skills.Contains(skill))
        {
            return;
        }
        
        Owner.GetNetwork().SendPacket($"u_pet {Entity.Id} {(int)target.EntityType} {target.Id} {skill.Id} {Entity.Position.X} {Entity.Position.Y}");
    }

    public void AttackSelf(NosmateSkill skill)
    {
        Attack(Entity, skill);
    }
    
    private async Task Walk(Position destination, CancellationToken cancellationToken)
    {
        var positiveX = destination.X > Entity.Position.X;
        var positiveY = destination.Y > Entity.Position.Y;
        
        while(!Entity.Position.Equals(destination) && !cancellationToken.IsCancellationRequested)
        {
            var distance = Entity.Position.Distance(destination);

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
            
            var x = (positiveX ? 1 : -1) * stepX + Entity.Position.X;
            var y = (positiveY ? 1 : -1) * stepY + Entity.Position.Y;

            var target = new Position(x, y);

            if (!Entity.Map.IsWalkable(target))
            {
                break;
            }
            
            Owner.GetBridge().WalkNosmate(target, Entity.Speed);

            try
            {
                await Task.Delay((stepX + stepY) * (2000 / Entity.Speed), cancellationToken);
            }
            catch (TaskCanceledException) { }
            finally
            {
                Entity.Position = target;
            }
        }
    }
}