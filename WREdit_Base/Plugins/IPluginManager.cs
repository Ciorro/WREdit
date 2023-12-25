using WREdit.Base.Properties;

namespace WREdit.Base.Plugins
{
    public interface IPluginManager
    {
        IEnumerable<Type> Processors { get; }
        IEnumerable<Type> Properties { get; }
        void InitializePlugins();
    }
}
