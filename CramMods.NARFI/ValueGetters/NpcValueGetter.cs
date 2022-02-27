using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramMods.NARFI.ValueGetters
{
    public partial class ValueGetter
    {
        public IFieldValue? GetNpcFieldValue(INpcGetter npc, Field field, FieldPath remainingPath)
        {
            return field switch
            {
                var _ when field.Equals(Field.Gender) => new SingleFieldValue<Gender>(GetNpcGender(npc)),
                var _ when field.Equals(Field.Unique) => new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Unique)),
                var _ when field.Equals(Field.Essential) => new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Essential)),
                var _ when field.Equals(Field.Protected) => new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Protected)),
                var _ when field.Equals(Field.OppositeGenderAnimations) => new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.OppositeGenderAnims)),
                var _ when field.Equals(Field.MagickaOffset) => new SingleFieldValue<int>(npc.Configuration.MagickaOffset),
                var _ when field.Equals(Field.StaminaOffset) => new SingleFieldValue<int>(npc.Configuration.StaminaOffset),
                var _ when field.Equals(Field.Level) => (!npc.Configuration.Level.GetType().IsAssignableTo(typeof(INpcLevel))) ? null : new SingleFieldValue<int>(((INpcLevel)npc.Configuration.Level).Level),
                var _ when field.Equals(Field.LevelMultiplier) => (!npc.Configuration.Level.GetType().IsAssignableTo(typeof(INpcLevel))) ? null : new SingleFieldValue<float>(((IPcLevelMult)npc.Configuration.Level).LevelMult),
                var _ when field.Equals(Field.CalculatedMinimumLevel) => new SingleFieldValue<int>(npc.Configuration.CalcMinLevel),
                var _ when field.Equals(Field.CalculatedMaximumLevel) => new SingleFieldValue<int>(npc.Configuration.CalcMaxLevel),
                var _ when field.Equals(Field.HealthOffset) => new SingleFieldValue<int>(npc.Configuration.HealthOffset),
                var _ when field.Equals(Field.BleedoutOverride) => new SingleFieldValue<int>(npc.Configuration.BleedoutOverride),

                var _ when field.Equals(Field.Aggression) => new SingleFieldValue<Aggression>(npc.AIData.Aggression),
                var _ when field.Equals(Field.Confidence) => new SingleFieldValue<Confidence>(npc.AIData.Confidence),
                var _ when field.Equals(Field.EnergyLevel) => new SingleFieldValue<int>(npc.AIData.EnergyLevel),
                var _ when field.Equals(Field.Responsibility) => new SingleFieldValue<Responsibility>(npc.AIData.Responsibility),
                var _ when field.Equals(Field.Mood) => new SingleFieldValue<Mood>(npc.AIData.Mood),
                var _ when field.Equals(Field.Assistance) => new SingleFieldValue<Assistance>(npc.AIData.Assistance),

                var _ when field.Equals(Field.Skill_OneHanded) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.OneHanded]),
                var _ when field.Equals(Field.Skill_TwoHanded) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.TwoHanded]),
                var _ when field.Equals(Field.Skill_Archery) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Archery]),
                var _ when field.Equals(Field.Skill_Block) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Block]),
                var _ when field.Equals(Field.Skill_Smithing) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Smithing]),
                var _ when field.Equals(Field.Skill_HeavyArmor) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.HeavyArmor]),
                var _ when field.Equals(Field.Skill_LightArmor) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.LightArmor]),
                var _ when field.Equals(Field.Skill_Pickpocket) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Pickpocket]),
                var _ when field.Equals(Field.Skill_Lockpicking) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Lockpicking]),
                var _ when field.Equals(Field.Skill_Sneak) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Sneak]),
                var _ when field.Equals(Field.Skill_Alchemy) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Alchemy]),
                var _ when field.Equals(Field.Skill_Speech) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Speech]),
                var _ when field.Equals(Field.Skill_Alteration) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Alteration]),
                var _ when field.Equals(Field.Skill_Conjuration) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Conjuration]),
                var _ when field.Equals(Field.Skill_Destruction) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Destruction]),
                var _ when field.Equals(Field.Skill_Illusion) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Illusion]),
                var _ when field.Equals(Field.Skill_Restoration) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Restoration]),
                var _ when field.Equals(Field.Skill_Enchanting) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillValues[Skill.Enchanting]),

                var _ when field.Equals(Field.SkillOffset_OneHanded) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.OneHanded]),
                var _ when field.Equals(Field.SkillOffset_TwoHanded) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.TwoHanded]),
                var _ when field.Equals(Field.SkillOffset_Archery) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Archery]),
                var _ when field.Equals(Field.SkillOffset_Block) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Block]),
                var _ when field.Equals(Field.SkillOffset_Smithing) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Smithing]),
                var _ when field.Equals(Field.SkillOffset_HeavyArmor) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.HeavyArmor]),
                var _ when field.Equals(Field.SkillOffset_LightArmor) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.LightArmor]),
                var _ when field.Equals(Field.SkillOffset_Pickpocket) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Pickpocket]),
                var _ when field.Equals(Field.SkillOffset_Lockpicking) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Lockpicking]),
                var _ when field.Equals(Field.SkillOffset_Sneak) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Sneak]),
                var _ when field.Equals(Field.SkillOffset_Alchemy) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Alchemy]),
                var _ when field.Equals(Field.SkillOffset_Speech) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Speech]),
                var _ when field.Equals(Field.SkillOffset_Alteration) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Alteration]),
                var _ when field.Equals(Field.SkillOffset_Conjuration) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Conjuration]),
                var _ when field.Equals(Field.SkillOffset_Destruction) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Destruction]),
                var _ when field.Equals(Field.SkillOffset_Illusion) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Illusion]),
                var _ when field.Equals(Field.SkillOffset_Restoration) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Restoration]),
                var _ when field.Equals(Field.SkillOffset_Enchanting) => (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(npc.PlayerSkills.SkillOffsets[Skill.Enchanting]),

                var _ when field.Equals(Field.Class) => GetFieldValue(npc.Class.Resolve(_linkCache), remainingPath),
                var _ when field.Equals(Field.CrimeFaction) => npc.CrimeFaction.IsNull ? null : GetFieldValue(npc.CrimeFaction.Resolve(_linkCache), remainingPath),
                var _ when field.Equals(Field.DefaultOutfit) => npc.DefaultOutfit.IsNull ? null : GetFieldValue(npc.DefaultOutfit.Resolve(_linkCache), remainingPath),
                var _ when field.Equals(Field.Faction) => FieldValueConverter.ToArray(npc.Factions.Select(f => GetFieldValue(f.Faction.Resolve(_linkCache), remainingPath.Clone()))),

                var _ when field.Equals(Field.HairColor) => 
                    npc.HairColor.IsNull 
                        ? FieldValueConverter.ToSingle( (IGenderedFieldValue?)GetFieldValue(npc.Race.Resolve(_linkCache), remainingPath.Clone().AddToFront(field)), GetNpcGender(npc)) ?? null 
                        : GetFieldValue(npc.HairColor.Resolve(_linkCache), remainingPath),

                var _ when field.Equals(Field.HeadPart) => FieldValueConverter.ToArray(npc.HeadParts.Select(f => GetFieldValue(f.Resolve(_linkCache), remainingPath.Clone()))),
                var _ when field.Equals(Field.HeadTexture) => npc.HeadTexture.IsNull ? null : GetFieldValue(npc.HeadTexture.Resolve(_linkCache), remainingPath),
                var _ when field.Equals(Field.Height) => new SingleFieldValue<float>(npc.Height),
                var _ when field.Equals(Field.Item) => (npc.Items == null) ? null : FieldValueConverter.ToArray(npc.Items.Select(i => GetFieldValue(i.Item.Item.Resolve(_linkCache), remainingPath.Clone()))),
                var _ when field.Equals(Field.Keyword) => (npc.Keywords == null) ? null : FieldValueConverter.ToArray(npc.Keywords.Select(k => GetFieldValue(k.Resolve(_linkCache), remainingPath.Clone()))),
                var _ when field.Equals(Field.Name) => ((npc.Name == null) || (npc.Name.String == null)) ? null : new SingleFieldValue<string>(npc.Name.String),
                var _ when field.Equals(Field.Race) => GetFieldValue(npc.Race.Resolve(_linkCache), remainingPath),
                var _ when field.Equals(Field.ShortName) => ((npc.ShortName == null) || (npc.ShortName.String == null)) ? null : new SingleFieldValue<string>(npc.ShortName.String),
                var _ when field.Equals(Field.SleepingOutfit) => npc.SleepingOutfit.IsNull ? null : GetFieldValue(npc.SleepingOutfit.Resolve(_linkCache), remainingPath),

                var _ when field.Equals(Field.Voice) =>
                    npc.Voice.IsNull
                        ? FieldValueConverter.ToSingle((IGenderedFieldValue?)GetFieldValue(npc.Race.Resolve(_linkCache), remainingPath.Clone().AddToFront(field)), GetNpcGender(npc)) ?? null
                        : GetFieldValue(npc.Voice.Resolve(_linkCache), remainingPath),

                var _ when field.Equals(Field.Weight) => new SingleFieldValue<float>(npc.Weight),

                var _ when field.Equals(Field.WornArmor) => throw new NotImplementedException(),

                _ => throw new NotImplementedException($"NPC.{field}"),
            };
        }

        public Gender GetNpcGender(INpcGetter npc) => npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Female) ? Gender.Female : Gender.Male;
    }
}
