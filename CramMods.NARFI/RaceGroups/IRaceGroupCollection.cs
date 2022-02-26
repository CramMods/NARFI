using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.RaceGroups
{
    public interface IRaceGroupCollection : IEnumerable<IRaceGroup>
    {
        public IList<IRaceGroup> Find(IRaceGetter race);
        public IList<IRaceGroup> RaceGroups { get; }
    }
}
