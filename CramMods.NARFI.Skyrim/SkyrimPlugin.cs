using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;
using System.Reflection;

namespace CramMods.NARFI
{
    public class SkyrimPlugin : Plugin
    {
        private List<Field> _fields = new();
        public override IReadOnlyList<Field> Fields => _fields.AsReadOnly();

        private List<IFieldValueGetter> _getters = new();
        public override IReadOnlyList<IFieldValueGetter> Getters => _getters.AsReadOnly();

        public SkyrimPlugin() : base("Skyrim") 
        {
            _fields.AddRange(Skyrim.NpcFields.All());
            _getters.Add(new Skyrim.NpcFieldValueGetter());

            _fields.AddRange(Skyrim.RaceFields.All());
            _getters.Add(new Skyrim.RaceFieldValueGetter());

            _fields = _fields
                .DistinctBy(f => f.Name)
                .ToList();
        }
    }
}
