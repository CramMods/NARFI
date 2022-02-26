using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using System.Collections;

namespace CramMods.NARFI.RaceGroups
{
    public class RaceGroupCollection : IRaceGroupCollection
    {
        private List<IRaceGroup> _raceGroups;
        public IList<IRaceGroup> RaceGroups => _raceGroups;

        public IList<IRaceGroup> Find(IRaceGetter race) => _raceGroups.FindAll(rg => rg.Contains(race));

        public IEnumerator<IRaceGroup> GetEnumerator() => _raceGroups.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _raceGroups.GetEnumerator();

        public RaceGroupCollection(IEnumerable<IRaceGroup> raceGroups) => _raceGroups = raceGroups.ToList();
        public RaceGroupCollection() : this(new List<IRaceGroup>()) { }


        public static IRaceGroupCollection FromDictionary(Dictionary<string, IEnumerable<string>> raceDictionary, IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> state)
        {
            List<IRaceGetter> allRaces = state.LoadOrder.PriorityOrder.Race().WinningOverrides().ToList();
            List<RaceNameGroup> nameGroups = raceDictionary.Select(rde => new RaceNameGroup(rde)).ToList();
            List<RaceGroup> raceGroups = nameGroups.ConvertAll(ng => ng.Flatten(allRaces, nameGroups));
            return new RaceGroupCollection(raceGroups);
        }

        public static IRaceGroupCollection FromDictionary(Dictionary<string, string[]> raceDictionary, IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> state) => 
            FromDictionary(raceDictionary.ToDictionary(e => e.Key, e => (IEnumerable<string>)e.Value.ToList()), state);

        private class RaceNameGroup
        {
            private string _name;
            public string Name => _name;

            private List<string> _raceNames;
            public List<string> RaceNames => _raceNames;

            public RaceNameGroup(string name, IEnumerable<string> raceNames)
            {
                _name = name;
                _raceNames = raceNames.ToList();
            }

            public RaceNameGroup(KeyValuePair<string, IEnumerable<string>> pair) : this(pair.Key, pair.Value) { }

            public RaceGroup Flatten(List<IRaceGetter> allRaces, List<RaceNameGroup> otherGroups)
            {
                List<RaceNameGroup> subGroups = otherGroups.FindAll(og => _raceNames.Contains(og.Name, StringComparer.InvariantCultureIgnoreCase));
                List<IRaceGetter> races = allRaces
                    .FindAll(r => (r.EditorID != null) && _raceNames.Contains(r.EditorID, StringComparer.InvariantCultureIgnoreCase))
                    .Concat(subGroups.SelectMany(sg => sg.Flatten(allRaces, otherGroups).Races))
                    .OrderBy(r => r.EditorID)
                    .ToList();
                return new RaceGroup(_name, races);
            }
        }
    }
}
