using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.RaceGroups
{
    public interface IRaceGroup
    {
        public string Name { get; }
        public IReadOnlyCollection<IRaceGetter> Races { get; }
        public bool Contains(IRaceGetter race);
    }
}
