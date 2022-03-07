using CramMods.NARFI.Fields;

namespace CramMods.NARFI.Skyrim
{
    public static class NpcFields
    {
        public static Field RaceGroup = new(nameof(RaceGroup));

        public static Field Gender = new(nameof(Gender));
        public static Field Unique = new(nameof(Unique));
        public static Field Essential = new(nameof(Essential));
        public static Field Protected = new(nameof(Protected));
        public static Field OppositeGenderAnimations = new(nameof(OppositeGenderAnimations), "OppositeGenderAnims", "OppositeAnims");

        public static Field HealthOffset = new(nameof(HealthOffset));
        public static Field MagickaOffset = new(nameof(MagickaOffset));
        public static Field StaminaOffset = new(nameof(StaminaOffset));
        public static Field SpeedMultiplier = new(nameof(SpeedMultiplier), "SpeedMult");
        public static Field BleedoutOverride = new(nameof(BleedoutOverride));

        public static Field Level = new(nameof(Level));
        public static Field LevelMultiplier = new(nameof(LevelMultiplier), "LevelMult");
        public static Field CalculatedMinimumLevel = new(nameof(CalculatedMinimumLevel), "CalcMinLevel", "MinimumLevel", "MinLevel");
        public static Field CalculatedMaximumLevel = new(nameof(CalculatedMaximumLevel), "CalcMaxLevel", "MaximumLevel", "MaxLevel");

        public static Field Aggression = new(nameof(Aggression));
        public static Field Confidence = new(nameof(Confidence));
        public static Field Responsibility = new(nameof(Responsibility));
        public static Field Mood = new(nameof(Mood));
        public static Field Assistance = new(nameof(Assistance));
        public static Field EnergyLevel = new(nameof(EnergyLevel), "Energy");

        public static Field Skill = new(nameof(Skill));
        public static Field SkillOffset = new(nameof(SkillOffset));

        public static Field Class = new(nameof(Class), "CNAM");
        public static Field CrimeFaction = new(nameof(CrimeFaction), "CRIF");
        public static Field DefaultOutfit = new(nameof(DefaultOutfit), "DOFT");
        public static Field Faction = new(nameof(Faction), "FNAM");
        public static Field HairColor = new(nameof(HairColor), "HCLF");
        public static Field HeadPart = new(nameof(HeadPart), "PNAM");
        public static Field HeadTexture = new(nameof(HeadTexture), "FTST");
        public static Field Height = new(nameof(Height), "NAM6");
        public static Field Item = new(nameof(Item));
        public static Field Keyword = new(nameof(Keyword), "KWDA");
        public static Field Name = new(nameof(Name), "FULL", "FullName");
        public static Field Race = new(nameof(Race), "RNAM");
        public static Field ShortName = new(nameof(ShortName), "SHRT");
        public static Field SleepingOutfit = new(nameof(SleepingOutfit), "SOFT");
        public static Field Voice = new(nameof(Voice), "VTCK");
        public static Field Weight = new(nameof(Weight), "NAM7");
        public static Field WornArmor = new(nameof(WornArmor), "WNAM", "Skin");

        public static IReadOnlyList<Field> All() => typeof(NpcFields).GetFields()
            .Where(f => f.FieldType.IsAssignableTo(typeof(Field)))
            .Select(f => (Field)f.GetValue(null)!)
            .ToList().AsReadOnly();
    }

    public static class NpcSkillFields
    {
        public static Field OneHanded = new(nameof(OneHanded));
        public static Field TwoHanded = new(nameof(TwoHanded));
        public static Field Archery = new(nameof(Archery), "Marksman");
        public static Field Block = new(nameof(Block));
        public static Field Smithing = new(nameof(Smithing));
        public static Field HeavyArmor = new(nameof(HeavyArmor));
        public static Field LightArmor = new(nameof(LightArmor));
        public static Field Pickpocket = new(nameof(Pickpocket));
        public static Field Lockpicking = new(nameof(Lockpicking));
        public static Field Sneak = new(nameof(Sneak));
        public static Field Alchemy = new(nameof(Alchemy));
        public static Field Speech = new(nameof(Speech), "Speechcraft");
        public static Field Alteration = new(nameof(Alteration));
        public static Field Conjuration = new(nameof(Conjuration));
        public static Field Destruction = new(nameof(Destruction));
        public static Field Illusion = new(nameof(Illusion));
        public static Field Restoration = new(nameof(Restoration));
        public static Field Enchanting = new(nameof(Enchanting));
    }
}
