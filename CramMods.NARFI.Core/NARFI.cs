using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using CramMods.NARFI.Plugins;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Cache;
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
            foreach (var getter in plugin.Getters)
            {
                getter.SetMasterGetter(this);
                getter.SetLinkCache(_state.LinkCache);
            }
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

        public void SetMasterGetter(IFieldValueGetter master) => throw new NotImplementedException();
        public void SetLinkCache(ILinkCache linkCache) => throw new NotImplementedException();
        public bool CanGetFieldValue(IMajorRecordGetter record, Field field) => _getters.Any(getter => getter.CanGetFieldValue(record, field));
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, Field field, FieldPath remainingPath) => _getters.FirstOrDefault(getter => getter.CanGetFieldValue(record, field))?.GetFieldValue(record, field, remainingPath) ?? throw new NotImplementedException("No getter for this operation");
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, FieldPath path) => GetFieldValue(record, path.Dequeue(), path);

    }
}
