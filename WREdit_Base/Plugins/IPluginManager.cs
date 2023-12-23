namespace WREdit.Base.Plugins
{
    public interface IPluginManager
    {
        IEnumerable<Type> Processors { get; }
        void InitializePlugins();
    }
}
