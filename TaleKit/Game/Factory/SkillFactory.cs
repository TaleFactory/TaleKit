using TaleKit.Game.Combat;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public class SkillFactory
{
    public static Skill CreateSkill(int vnum)
    {
        var data = SkillRegistry.GetSkillData(vnum);
        if (data is null)
        {
            throw new InvalidOperationException();
        }

        return new Skill
        {
            VirtualNumber = vnum,
            Name = TranslationRegistry.GetTranslation(TranslationGroup.Skills, TaleKitSettings.Language, data.NameKey)  ?? "Undefined",
            Range = data.Range,
            Type = (SkillType) data.SkillType,
            CastId = data.CastId,
            ZoneRange = data.ZoneRange,
            HitType = (HitType) data.HitType,
            TargetType = (TargetType)data.TargetType,
            CastTime = data.CastTime
        };
    }
}