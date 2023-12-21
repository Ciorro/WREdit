namespace WREdit.Plugins
{
    internal interface IPluginManager
    {
        IEnumerable<Type> Processors { get; }
        void InitializePlugins();
    }
}
