using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.RaceGroups
{
    public class RaceGroup : IRaceGroup
    {
        private string _name;
        public string Name => _name;

        private List<IRaceGetter> _groups;
        public IReadOnlyCollection<IRaceGetter> Races => _groups.AsReadOnly();

        public bool Contains(IRaceGetter race) => _groups.Any(r => r.FormKey == race.FormKey);

        public RaceGroup(string name, IEnumerable<IRaceGetter> races)
        {
            _name = name;
            _groups = races.ToList();
        }

        public RaceGroup(string name) : this(name, new List<IRaceGetter>()) { }

        public override string ToString() => $"{_name} [{_groups.Count}]";
    }
}
