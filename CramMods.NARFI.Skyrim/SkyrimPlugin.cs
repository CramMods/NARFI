using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.Plugins;
using CramMods.NARFI.Skyrim;

namespace CramMods.NARFI
{
    public class SkyrimPlugin : Plugin
    {
        private List<Field> _fields = new();
        public override IReadOnlyList<Field> Fields => _fields.AsReadOnly();

        private List<IFieldValueGetter> _getters = new();
        public override IReadOnlyList<IFieldValueGetter> Getters => _getters.AsReadOnly();

        private List<RaceGroup> _raceGroups = new();
        public void SetRaceGroups(List<RaceGroup> raceGroups)
        {
            _raceGroups.Clear();
            _raceGroups.AddRange(raceGroups);
        }

        public SkyrimPlugin() : base("Skyrim") 
        {
            _fields.AddRange(NpcFields.All());
            _getters.Add(new NpcFieldValueGetter(_raceGroups));

            _fields.AddRange(RaceFields.All());
            _getters.Add(new RaceFieldValueGetter());

            _fields = _fields
                .DistinctBy(f => f.Name)
                .ToList();
        }
    }
}
