using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.Skyrim
{
    public class RaceGroup
    {
        private string _name;
        public string Name => _name;

        private List<IRaceGetter> _races = new();
        public IReadOnlyList<IRaceGetter> Races => _races.AsReadOnly();

        public IReadOnlyList<string> RaceIds => _races
            .Select(r => r.EditorID)
            .Where(n => n != null)
            .Select(r => r!)
            .ToList().AsReadOnly();

        public RaceGroup(string name, IEnumerable<IRaceGetter> races) => (_name, _races) = (name, races.ToList());
        public RaceGroup(string name, params IRaceGetter[] races) : this(name, races.ToList()) { }

        public bool Contains(IRaceGetter race) => _races.Any(r => r.EditorID?.Equals(race.EditorID, StringComparison.InvariantCultureIgnoreCase) ?? false);
        public bool Contains(string raceId) => RaceIds.Any(n => n.Equals(raceId, StringComparison.InvariantCultureIgnoreCase));

        public override string ToString() => $"{_name} [{_races.Count}]";


        public static IEnumerable<RaceGroup> FromIdDictionary(IDictionary<string, string[]> raceIdDict, IEnumerable<IRaceGetter> allRaces)
        {
            List<RaceGroup> raceGroups = new();
            foreach (KeyValuePair<string, string[]> entry in raceIdDict) raceGroups.Add(FromIds(entry.Key, entry.Value, raceIdDict, allRaces));
            return raceGroups;
        }

        public static RaceGroup FromIds(string name, string[] names, IDictionary<string, string[]> raceIdDict, IEnumerable<IRaceGetter> allRaces)
        {
            Dictionary<string, string[]> others = new(raceIdDict, StringComparer.InvariantCultureIgnoreCase);
            List<IRaceGetter> outputRaces = new();

            foreach (string rname in names)
            {
                if (others.ContainsKey(rname))
                {
                    RaceGroup otherGroup = FromIds(rname, others[rname], raceIdDict, allRaces);
                    outputRaces.AddRange(otherGroup.Races);
                } 
                else
                {
                    IRaceGetter? race = allRaces.FirstOrDefault(race => race.EditorID?.Equals(rname, StringComparison.InvariantCultureIgnoreCase) ?? false);
                    if (race != null) outputRaces.Add(race);
                }
            }

            outputRaces = outputRaces
                .DistinctBy(race => race.FormKey.ToString())
                .OrderBy(race => race.FormKey.ToString())
                .ToList();

            return new(name, outputRaces);
        }
    }
}
