using CramMods.NARFI.Fields;

namespace CramMods.NARFI.Skyrim
{
    public static class RaceFields
    {
        public static Field Description = new(nameof(Description), "DESC");
        public static Field HairColor = new(nameof(HairColor), "HCLF");
        public static Field Keyword = new(nameof(Keyword), "KWDA");
        public static Field Name = new(nameof(Name), "FULL", "FullName");
        public static Field Voice = new(nameof(Voice), "VTCK");
        public static Field WornArmor = new(nameof(WornArmor), "WNAM", "Skin");

        public static IReadOnlyList<Field> All() => typeof(RaceFields).GetFields()
            .Where(f => f.FieldType.IsAssignableTo(typeof(Field)))
            .Select(f => (Field)f.GetValue(null)!)
            .ToList().AsReadOnly();
    }
}
