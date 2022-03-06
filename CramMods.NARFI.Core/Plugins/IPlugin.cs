using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using Mutagen.Bethesda.Environments;

namespace CramMods.NARFI.Plugins
{
    public interface IPlugin
    {
        public void SetGameEnvironmentState(IGameEnvironmentState state);
        public string Name { get; }
        public IEnumerable<Field> Fields { get; }
        public IEnumerable<IFieldValueGetter> Getters { get; }
    }
}