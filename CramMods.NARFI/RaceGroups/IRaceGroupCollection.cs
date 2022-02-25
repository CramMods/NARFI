using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.RaceGroups
{
    public interface IRaceGroupCollection : IEnumerable<IRaceGroup>
    {
        public IEnumerable<IRaceGroup> Find(IRaceGetter race);
    }
}
