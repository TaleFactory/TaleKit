using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Characters;

public class PetSki : IPacket
{
    public List<int> Skills { get; init; }
}

public class PetSkillBuilder : PacketBuilder<PetSki>
{
    public override string Header { get; } = "petski";
    
    protected override PetSki CreatePacket(string[] body)
    {
        var skills = new List<int>();

        if (body[0] != "-2")
        {
            foreach (var skillId in body)
            {
                var parsed = skillId.ToInt();
                if (parsed > 0)
                {
                    skills.Add(parsed);
                }
            }
        }

        return new PetSki
        {
            Skills = skills
        };
    }
}

public class PetSkillProcessor : PacketProcessor<PetSki>
{
    protected override void Process(Session session, PetSki packet)
    {
        if (session.Character.Nosmate == null)
        {
            return;
        }

        session.Character.Nosmate.Skills = packet.Skills
            .Select(x => new NosmateSkill 
            { 
                VirtualNumber = x 
            })
            .ToList();
    }
}