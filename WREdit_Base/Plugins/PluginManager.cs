using System.Reflection;
using WREdit.Base.Processing;
using WREdit.Base.Properties;

namespace WREdit.Base.Plugins
{
    public class PluginManager : IPluginManager
    {
        private readonly string _pluginsDirectory;
        public IEnumerable<Type> Processors { get; private set; }
        public IEnumerable<Type> Properties { get; private set; }

        public PluginManager(string pluginsDirectory)
        {
            _pluginsDirectory = Path.GetFullPath(pluginsDirectory);
            Processors = Enumerable.Empty<Type>();
            Properties = Enumerable.Empty<Type>();
        }

        public void InitializePlugins()
        {
            if (Directory.Exists(_pluginsDirectory))
            {
                var plugins = Directory.GetFiles(_pluginsDirectory, "*.dll").
                              Select(Assembly.LoadFile);
                Processors = plugins.SelectMany(InitializeProcessors);
                Properties = plugins.SelectMany(InitializeProperties);
            }
        }

        private IEnumerable<Type> InitializeProcessors(Assembly plugin)
        {
            var types = plugin.GetTypes();

            return types.Where(type =>
            {
                return type.IsAssignableTo(typeof(IGameObjectProcessor));
            });
        }

        private IEnumerable<Type> InitializeProperties(Assembly plugin)
        {
            var types = plugin.GetTypes();

            return types.Where(type =>
            {
                return type.IsAssignableTo(typeof(IProcessorProperty)) && !type.IsAbstract;
            });
        }
    }
}
