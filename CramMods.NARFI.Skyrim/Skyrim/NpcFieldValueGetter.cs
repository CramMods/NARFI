using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.Skyrim
{
    public class NpcFieldValueGetter : IFieldValueGetter
    {
        private IFieldValueGetter? _master;
        public void SetMasterGetter(IFieldValueGetter master) => _master = master;

        private ILinkCache<ISkyrimMod, ISkyrimModGetter>? _linkCache;
        public void SetLinkCache(ILinkCache linkCache) => _linkCache = (linkCache as ILinkCache<ISkyrimMod, ISkyrimModGetter>)!;

        public bool CanGetFieldValue(IMajorRecordGetter record, Field field)
        {
            if (!record.GetType().IsAssignableTo(typeof(INpcGetter))) return false;
            return NpcFields.All().Any(f => f.Equals(field));
        }


        public IFieldValue? GetFieldValue(INpcGetter npc, Field field, FieldPath remainingPath)
        {
            if (field.Equals(NpcFields.Gender)) return new SingleFieldValue<Gender>(GetGender(npc));
            if (field.Equals(NpcFields.Unique)) return new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Unique));
            if (field.Equals(NpcFields.Essential)) return new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Essential));
            if (field.Equals(NpcFields.Protected)) return new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Protected));
            if (field.Equals(NpcFields.OppositeGenderAnimations)) return new SingleFieldValue<bool>(npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.OppositeGenderAnims));

            if (field.Equals(NpcFields.HealthOffset)) return new SingleFieldValue<int>(npc.Configuration.HealthOffset);
            if (field.Equals(NpcFields.MagickaOffset)) return new SingleFieldValue<int>(npc.Configuration.MagickaOffset);
            if (field.Equals(NpcFields.StaminaOffset)) return new SingleFieldValue<int>(npc.Configuration.StaminaOffset);
            if (field.Equals(NpcFields.SpeedMultiplier)) return new SingleFieldValue<int>(npc.Configuration.SpeedMultiplier);
            if (field.Equals(NpcFields.BleedoutOverride)) return new SingleFieldValue<int>(npc.Configuration.BleedoutOverride);

            if (field.Equals(NpcFields.Level)) return !npc.Configuration.Level.GetType().IsAssignableTo(typeof(INpcLevel)) ? null : new SingleFieldValue<int>(((INpcLevel)npc.Configuration.Level).Level);
            if (field.Equals(NpcFields.LevelMultiplier)) return !npc.Configuration.Level.GetType().IsAssignableTo(typeof(IPcLevelMult)) ? null : new SingleFieldValue<float>(((IPcLevelMult)npc.Configuration.Level).LevelMult);
            if (field.Equals(NpcFields.CalculatedMinimumLevel)) return new SingleFieldValue<int>(npc.Configuration.CalcMinLevel);
            if (field.Equals(NpcFields.CalculatedMaximumLevel)) return new SingleFieldValue<int>(npc.Configuration.CalcMaxLevel);

            if (field.Equals(NpcFields.Aggression)) return new SingleFieldValue<Aggression>(npc.AIData.Aggression);
            if (field.Equals(NpcFields.Confidence)) return new SingleFieldValue<Confidence>(npc.AIData.Confidence);
            if (field.Equals(NpcFields.Responsibility)) return new SingleFieldValue<Responsibility>(npc.AIData.Responsibility);
            if (field.Equals(NpcFields.Mood)) return new SingleFieldValue<Mood>(npc.AIData.Mood);
            if (field.Equals(NpcFields.Assistance)) return new SingleFieldValue<Assistance>(npc.AIData.Assistance);
            if (field.Equals(NpcFields.EnergyLevel)) return new SingleFieldValue<int>(npc.AIData.EnergyLevel);

            if (field.Equals(NpcFields.Skill)) return (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(GetNpcSkill(npc, remainingPath.Dequeue(), false));
            if (field.Equals(NpcFields.SkillOffset)) return (npc.PlayerSkills == null) ? null : new SingleFieldValue<int>(GetNpcSkill(npc, remainingPath.Dequeue(), true));

            if (field.Equals(NpcFields.Class)) return _master!.GetFieldValue(npc.Class.Resolve(_linkCache!), remainingPath);
            if (field.Equals(NpcFields.CrimeFaction)) return _master!.GetFieldValue(npc.CrimeFaction.Resolve(_linkCache!), remainingPath);

            if (field.Equals(NpcFields.DefaultOutfit)) return npc.DefaultOutfit.IsNull
                    ? _master!.GetFieldValue(npc.Race.Resolve(_linkCache!), remainingPath.AddToFront(field))
                    : _master!.GetFieldValue(npc.DefaultOutfit.Resolve(_linkCache!), remainingPath);

            if (field.Equals(NpcFields.Faction)) return FieldValueConverter.ToArray(npc.Factions.Select(f => _master!.GetFieldValue(f.Faction.Resolve(_linkCache!), remainingPath.Clone())));

            if (field.Equals(NpcFields.HairColor)) return npc.HairColor.IsNull
                    ? FieldValueConverter.ToSingle(_master!.GetFieldValue(npc.Race.Resolve(_linkCache!), remainingPath.AddToFront(field)), GetGender(npc))
                    : _master!.GetFieldValue(npc.HairColor.Resolve(_linkCache!), remainingPath);

            if (field.Equals(NpcFields.HeadPart)) return FieldValueConverter.ToArray(npc.HeadParts.Select(p => _master!.GetFieldValue(p.Resolve(_linkCache!), remainingPath.Clone())));
            if (field.Equals(NpcFields.HeadTexture)) return npc.HeadTexture.IsNull ? null : _master!.GetFieldValue(npc.HeadTexture.Resolve(_linkCache!), remainingPath);
            if (field.Equals(NpcFields.Height)) return new SingleFieldValue<float>(npc.Height);
            if (field.Equals(NpcFields.Item)) return (npc.Items == null) ? null : FieldValueConverter.ToArray(npc.Items.Select(i => _master!.GetFieldValue(i.Item.Item.Resolve(_linkCache!), remainingPath.Clone())));
            if (field.Equals(NpcFields.Keyword)) return (npc.Keywords == null) ? null : FieldValueConverter.ToArray(npc.Keywords.Select(k => _master!.GetFieldValue(k.Resolve(_linkCache!), remainingPath.Clone())));
            if (field.Equals(NpcFields.Name)) return new SingleFieldValue<string>(npc.Name?.String) ?? null;
            if (field.Equals(NpcFields.Race)) return _master!.GetFieldValue(npc.Race.Resolve(_linkCache!), remainingPath);
            if (field.Equals(NpcFields.ShortName)) return new SingleFieldValue<string>(npc.ShortName?.String) ?? null;

            if (field.Equals(NpcFields.SleepingOutfit)) return npc.SleepingOutfit.IsNull
                    ? _master!.GetFieldValue(npc.Race.Resolve(_linkCache!), remainingPath.AddToFront(field))
                    : _master!.GetFieldValue(npc.SleepingOutfit.Resolve(_linkCache!), remainingPath);

            if (field.Equals(NpcFields.Voice)) return npc.Voice.IsNull
                    ? FieldValueConverter.ToSingle(_master!.GetFieldValue(npc.Race.Resolve(_linkCache!), remainingPath.AddToFront(field)), GetGender(npc))
                    : _master!.GetFieldValue(npc.Voice.Resolve(_linkCache!), remainingPath);

            if (field.Equals(NpcFields.Weight)) return new SingleFieldValue<float>(npc.Weight);

            if (field.Equals(NpcFields.WornArmor)) return npc.WornArmor.IsNull
                    ? _master!.GetFieldValue(npc.Race.Resolve(_linkCache!), remainingPath.AddToFront(field))
                    : _master!.GetFieldValue(npc.WornArmor.Resolve(_linkCache!), remainingPath);

            throw new NotImplementedException();
        }

        public IFieldValue? GetFieldValue(IMajorRecordGetter record, Field field, FieldPath remainingPath) => GetFieldValue((INpcGetter)record, field, remainingPath);
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, FieldPath path) => GetFieldValue(record, path.Dequeue(), path);

        private Gender GetGender(INpcGetter npc) => npc.Configuration.Flags.HasFlag(NpcConfiguration.Flag.Female) ? Gender.Female : Gender.Male;

        private int GetNpcSkill(INpcGetter npc, Field skill, bool offset = false)
        {
            if (npc.PlayerSkills == null) throw new Exception("PlayerSkills is null. How'd you get here?");
            IReadOnlyDictionary<Skill, byte> data = offset ? npc.PlayerSkills.SkillOffsets : npc.PlayerSkills.SkillValues;

            return skill switch 
            { 
                var _ when skill.Equals(NpcSkillFields.OneHanded) => data[Skill.OneHanded],
                var _ when skill.Equals(NpcSkillFields.TwoHanded) => data[Skill.TwoHanded],
                var _ when skill.Equals(NpcSkillFields.Archery) => data[Skill.Archery],
                var _ when skill.Equals(NpcSkillFields.Block) => data[Skill.Block],
                var _ when skill.Equals(NpcSkillFields.Smithing) => data[Skill.Smithing],
                var _ when skill.Equals(NpcSkillFields.HeavyArmor) => data[Skill.HeavyArmor],
                var _ when skill.Equals(NpcSkillFields.LightArmor) => data[Skill.LightArmor],
                var _ when skill.Equals(NpcSkillFields.Pickpocket) => data[Skill.Pickpocket],
                var _ when skill.Equals(NpcSkillFields.Lockpicking) => data[Skill.Lockpicking],
                var _ when skill.Equals(NpcSkillFields.Sneak) => data[Skill.Sneak],
                var _ when skill.Equals(NpcSkillFields.Alchemy) => data[Skill.Alchemy],
                var _ when skill.Equals(NpcSkillFields.Speech) => data[Skill.Speech],
                var _ when skill.Equals(NpcSkillFields.Alteration) => data[Skill.Alteration],
                var _ when skill.Equals(NpcSkillFields.Conjuration) => data[Skill.Conjuration],
                var _ when skill.Equals(NpcSkillFields.Destruction) => data[Skill.Destruction],
                var _ when skill.Equals(NpcSkillFields.Illusion) => data[Skill.Illusion],
                var _ when skill.Equals(NpcSkillFields.Restoration) => data[Skill.Restoration],
                var _ when skill.Equals(NpcSkillFields.Enchanting) => data[Skill.Enchanting],
                _ => throw new NotImplementedException()
            };
        }
    }
}
