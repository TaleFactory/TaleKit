using TaleKit.Game;
using TaleKit.Game.Combat;
using TaleKit.Game.Factory;

namespace TaleKit.Network.Packet.Characters;

public class Ski : IPacket
{
    public List<int> Skills { get; set; }
}

public class SkiBuilder : PacketBuilder<Ski>
{
    public override string Header { get; } = "ski";
    
    protected override Ski CreatePacket(string[] body)
    {
        var skills = new List<int>();
        
        foreach (var entry in body.Skip(1))
        {
            var skillId = entry.Split('|');
            if (skillId.Length > 0)
            {
                skills.Add(Convert.ToInt32(skillId[0]));
                continue;
            }
                
            skills.Add(Convert.ToInt32(entry));
        }

        return new Ski
        {
            Skills = skills
        };
    }
}

public class SkiProcessor : PacketProcessor<Ski>
{
    protected override void Process(Session session, Ski packet)
    {
        session.Character.Skills = new HashSet<Skill>();

        foreach (var skillId in packet.Skills)
        {
            var skill = SkillFactory.CreateSkill(skillId);
            if (skill is null)
            {
                continue;
            }

            if (skill.Type == SkillType.Player)
            {
                session.Character.Skills.Add(skill);   
            }
        }
    }
}