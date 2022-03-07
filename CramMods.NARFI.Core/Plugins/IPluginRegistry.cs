namespace CramMods.NARFI.Plugins
{
    public interface IPluginRegistry
    {
        public void RegisterPlugin(IPlugin plugin);
        public void UnregisterPlugin(IPlugin plugin);
    }
}
