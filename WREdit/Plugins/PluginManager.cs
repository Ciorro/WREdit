using System.IO;
using System.Reflection;
using WREdit.Base.Models;

namespace WREdit.Plugins
{
    internal class PluginManager : IPluginManager
    {
        private readonly string _pluginsDirectory;
        private IEnumerable<Type>? _processors;

        public PluginManager(string pluginsDirectory)
        {
            _pluginsDirectory = Path.GetFullPath(pluginsDirectory);
        }

        public IEnumerable<Type> Processors
        {
            get => _processors ?? Enumerable.Empty<Type>();
        }

        public void InitializePlugins()
        {
            if (Directory.Exists(_pluginsDirectory))
            {
                var plugins = Directory.GetFiles(_pluginsDirectory, "*.dll").
                              Select(Assembly.LoadFile);
                _processors = plugins.SelectMany(InitializeProcessors);
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
