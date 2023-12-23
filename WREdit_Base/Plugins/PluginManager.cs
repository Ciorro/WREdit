using System.Reflection;
using WREdit.Base.Models;

namespace WREdit.Base.Plugins
{
    public class PluginManager : IPluginManager
    {
        private readonly string _pluginsDirectory;
        public IEnumerable<Type> Processors { get; private set; }

        public PluginManager(string pluginsDirectory)
        {
            _pluginsDirectory = Path.GetFullPath(pluginsDirectory);
            Processors = Enumerable.Empty<Type>();
        }

        public void InitializePlugins()
        {
            if (Directory.Exists(_pluginsDirectory))
            {
                var plugins = Directory.GetFiles(_pluginsDirectory, "*.dll").
                              Select(Assembly.LoadFile);
                Processors = plugins.SelectMany(InitializeProcessors);
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
    }
}
