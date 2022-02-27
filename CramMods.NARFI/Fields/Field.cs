using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramMods.NARFI.Fields
{
    public class Field : FieldBase
    {
        #region Static "enum" values

        public static Field EditorID = new(nameof(EditorID));
        public static Field EDID = EditorID;

        public static Field FormKey = new(nameof(FormKey));

        #region NPC Configuration
        public static Field Gender = new(nameof(Gender));
        public static Field Unique = new(nameof(Unique));
        public static Field Essential = new(nameof(Essential));
        public static Field Protected = new(nameof(Protected));

        public static Field OppositeGenderAnimations = new(nameof(OppositeGenderAnimations));
        public static Field OppositeGenderAnims = OppositeGenderAnimations;

        public static Field MagickaOffset = new(nameof(MagickaOffset));
        public static Field StaminaOffset = new(nameof(StaminaOffset));
        public static Field Level = new(nameof(Level));

        public static Field LevelMultiplier = new(nameof(LevelMultiplier));
        public static Field LevelMult = LevelMultiplier;

        public static Field CalculatedMinimumLevel = new(nameof(CalculatedMinimumLevel));
        public static Field CalcMinLevel = CalculatedMinimumLevel;

        public static Field CalculatedMaximumLevel = new(nameof(CalculatedMaximumLevel));
        public static Field CalcMaxLevel = CalculatedMaximumLevel;

        public static Field SpeedMultiplier = new(nameof(SpeedMultiplier));
        public static Field SpeedMult = SpeedMultiplier;

        public static Field HealthOffset = new(nameof(HealthOffset));
        public static Field BleedoutOverride = new(nameof(BleedoutOverride));
        #endregion

        #region NPC AI Data
        public static Field Aggression = new(nameof(Aggression));
        public static Field Confidence = new(nameof(Confidence));
        public static Field EnergyLevel = new(nameof(EnergyLevel));
        public static Field Responsibility = new(nameof(Responsibility));
        public static Field Mood = new(nameof(Mood));
        public static Field Assistance = new(nameof(Assistance));
        #endregion

        #region NPC Skills
        public static Field Skill_OneHanded = new(new(nameof(Skill_OneHanded)));
        public static Field Skill_TwoHanded = new(new(nameof(Skill_TwoHanded)));
        public static Field Skill_Archery = new(new(nameof(Skill_Archery)));
        public static Field Skill_Marksman = Skill_Archery;
        public static Field Skill_Block = new(new(nameof(Skill_Block)));
        public static Field Skill_Smithing = new(new(nameof(Skill_Smithing)));
        public static Field Skill_HeavyArmor = new(new(nameof(Skill_HeavyArmor)));
        public static Field Skill_LightArmor = new(new(nameof(Skill_LightArmor)));
        public static Field Skill_Pickpocket = new(new(nameof(Skill_Pickpocket)));
        public static Field Skill_Lockpicking = new(new(nameof(Skill_Lockpicking)));
        public static Field Skill_Sneak = new(new(nameof(Skill_Sneak)));
        public static Field Skill_Alchemy = new(new(nameof(Skill_Alchemy)));
        public static Field Skill_Speech = new(new(nameof(Skill_Speech)));
        public static Field Skill_Speechcraft = Skill_Speech;
        public static Field Skill_Alteration = new(new(nameof(Skill_Alteration)));
        public static Field Skill_Conjuration = new(new(nameof(Skill_Conjuration)));
        public static Field Skill_Destruction = new(new(nameof(Skill_Destruction)));
        public static Field Skill_Illusion = new(new(nameof(Skill_Illusion)));
        public static Field Skill_Restoration = new(new(nameof(Skill_Restoration)));
        public static Field Skill_Enchanting = new(new(nameof(Skill_Enchanting)));
        #endregion

        #region NPC Skill Offsets
        public static Field SkillOffset_OneHanded = new(new(nameof(SkillOffset_OneHanded)));
        public static Field SkillOffset_TwoHanded = new(new(nameof(SkillOffset_TwoHanded)));
        public static Field SkillOffset_Archery = new(new(nameof(SkillOffset_Archery)));
        public static Field SkillOffset_Marksman = SkillOffset_Archery;
        public static Field SkillOffset_Block = new(new(nameof(SkillOffset_Block)));
        public static Field SkillOffset_Smithing = new(new(nameof(SkillOffset_Smithing)));
        public static Field SkillOffset_HeavyArmor = new(new(nameof(SkillOffset_HeavyArmor)));
        public static Field SkillOffset_LightArmor = new(new(nameof(SkillOffset_LightArmor)));
        public static Field SkillOffset_Pickpocket = new(new(nameof(SkillOffset_Pickpocket)));
        public static Field SkillOffset_Lockpicking = new(new(nameof(SkillOffset_Lockpicking)));
        public static Field SkillOffset_Sneak = new(new(nameof(SkillOffset_Sneak)));
        public static Field SkillOffset_Alchemy = new(new(nameof(SkillOffset_Alchemy)));
        public static Field SkillOffset_Speech = new(new(nameof(SkillOffset_Speech)));
        public static Field SkillOffset_Speechcraft = SkillOffset_Speech;
        public static Field SkillOffset_Alteration = new(new(nameof(SkillOffset_Alteration)));
        public static Field SkillOffset_Conjuration = new(new(nameof(SkillOffset_Conjuration)));
        public static Field SkillOffset_Destruction = new(new(nameof(SkillOffset_Destruction)));
        public static Field SkillOffset_Illusion = new(new(nameof(SkillOffset_Illusion)));
        public static Field SkillOffset_Restoration = new(new(nameof(SkillOffset_Restoration)));
        public static Field SkillOffset_Enchanting = new(new(nameof(SkillOffset_Enchanting)));
        #endregion

        #region Race Data
        public static Field Playable = new(nameof(Playable));
        public static Field Swims = new(nameof(Swims));
        public static Field Walks = new(nameof(Walks));
        public static Field Flies = new(nameof(Flies));
        #endregion

        public static Field Class = new(nameof(Class));
        public static Field CNAM = Class;

        public static Field CrimeFaction = new(nameof(CrimeFaction));
        public static Field CRIF = CrimeFaction;

        public static Field DefaultOutfit = new(nameof(DefaultOutfit));
        public static Field DOFT = DefaultOutfit;

        public static Field Description = new(nameof(Description));
        public static Field DESC = Description;

        public static Field Faction = new(nameof(Faction));
        public static Field SNAM = Faction;

        public static Field HairColor = new(nameof(HairColor));
        public static Field HCLF = HairColor;

        public static Field HeadPart = new(nameof(HeadPart));
        public static Field PNAM = HeadPart;

        public static Field HeadTexture = new(nameof(HeadTexture));
        public static Field FTST = HeadTexture;

        public static Field Height = new(nameof(Height));
        public static Field NAM6 = Height;

        public static Field Item = new(nameof(Item));

        public static Field Keyword = new(nameof(Keyword));
        public static Field KWDA = Keyword;

        public static Field Name = new(nameof(Name));
        public static Field FNAM = Name;
        public static Field FullName = Name;

        public static Field Race = new(nameof(Race));
        public static Field RNAM = Race;

        public static Field ShortName = new(nameof(ShortName));
        public static Field SHRT = ShortName;

        public static Field SleepingOutfit = new(nameof(SleepingOutfit));
        public static Field SOFT = SleepingOutfit;

        public static Field Voice = new(nameof(Voice));
        public static Field VTCK = Voice;

        public static Field Weight = new(nameof(Weight));
        public static Field NAM7 = Weight;

        public static Field WornArmor = new(nameof(WornArmor));
        public static Field WNAM = WornArmor;
        public static Field Skin = WornArmor;


        #endregion

        public Field(string id) : base(id) { }

        public static IReadOnlyList<Field> GetAll() => GetAll<Field>();
        public static Field? Parse(string name) => Parse<Field>(name);

    }

}
