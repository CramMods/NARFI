using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using Mutagen.Bethesda.Environments;

namespace CramMods.NARFI.Plugins
{
    public abstract class Plugin : IPlugin
    {
        protected string _name;
        public string Name => _name;

        protected IGameEnvironmentState? _state;
        public void SetGameEnvironmentState(IGameEnvironmentState state) => _state = state;

        public abstract IEnumerable<Field> Fields { get; }
        public abstract IEnumerable<IFieldValueGetter> Getters { get; }
        public override string ToString() => Name;

        public Plugin(string name) => _name = name;
    }
}
