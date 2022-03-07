using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using CramMods.NARFI.Plugins;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI
{
    public class NARFI : IPluginRegistry, IFieldValueGetter
    {
        private List<Field> _fields = new();
        private List<IFieldValueGetter> _getters = new();

        private IGameEnvironmentState _state;
        public NARFI(IGameEnvironmentState state)
        {
            _state = state;
            RegisterPlugin(new Common());
        }

        private List<IPlugin> _plugins = new();
        public void RegisterPlugin(IPlugin plugin)
        {
            plugin.SetGameEnvironmentState(_state);
            _plugins.Add(plugin);
            RefreshPlugins();
        }
        public void UnregisterPlugin(IPlugin plugin) => _plugins.Remove(plugin);

        private void RefreshPlugins()
        {
            _fields = new();
            _getters = new();
            foreach (IPlugin plugin in _plugins)
            {
                _fields.AddRange(plugin.Fields);
                _getters.AddRange(plugin.Getters);
            }
        }

        public bool CanGet(IMajorRecordGetter record, Field field) => _getters.Any(getter => getter.CanGet(record, field));
        public IFieldValue? Get(IMajorRecordGetter record, Field field, FieldPath? remainingPath) => _getters.FirstOrDefault(getter => getter.CanGet(record, field))?.Get(record, field, remainingPath) ?? throw new NotImplementedException("No getter for this operation");
        public IFieldValue? Get(IMajorRecordGetter record, FieldPath path) => Get(record, path.Dequeue(), path);

    }
}
