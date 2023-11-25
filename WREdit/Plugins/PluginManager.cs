using System.IO;
using System.Reflection;
using WREdit.Base.Actions;

namespace WREdit.Plugins
{
    internal class PluginManager : IPluginManager
    {
        private readonly string _pluginsDirectory;
        private IEnumerable<IGameObjectAction>? _actions;

        public PluginManager(string pluginsDirectory)
        {
            _pluginsDirectory = Path.GetFullPath(pluginsDirectory);
        }

        public IEnumerable<IGameObjectAction> Actions
        {
            get => _actions ?? Enumerable.Empty<IGameObjectAction>();
        }

        public void InitializePlugins()
        { 
            var plugins = Directory.GetFiles(_pluginsDirectory, "*.dll").
                          Select(Assembly.LoadFile);
            _actions = plugins.SelectMany(InitializeActions);
        }

        private IEnumerable<IGameObjectAction> InitializeActions(Assembly plugin)
        {
            var types = plugin.GetTypes();

            var actionTypes = types.Where(type =>
            {
                return type.IsAssignableTo(typeof(IGameObjectAction));
            });

            var actions = actionTypes.Select(actionType =>
            {
                return (IGameObjectAction)Activator.CreateInstance(actionType)!;
            });

            return actions.Where(action => action is not null);
        }
    }
}
