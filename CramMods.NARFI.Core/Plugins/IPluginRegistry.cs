namespace CramMods.NARFI.Plugins
{
    public interface IPluginRegistry
    {
        public void Register(IPlugin plugin);
        public void Unregister(IPlugin plugin);
    }
}
