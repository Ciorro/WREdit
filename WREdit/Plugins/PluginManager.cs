using System.IO;
using System.Reflection;
using System.Windows;
using WREdit.Base.Attributes;
using WREdit.Base.Extensions;
using WREdit.Base.Models;
using WREdit.Base.Properties;

namespace WREdit.Plugins
{
    internal class PluginManager : IPluginManager
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

                foreach (var plugin in plugins)
                {
                    InitilizeProcessorProperties(plugin);
                }
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

        private void InitilizeProcessorProperties(Assembly assembly)
        {
            var propertyTypes = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IProcessorProperty)));

            foreach (var propertyType in propertyTypes)
            {
                InitializePropertyTemplates(propertyType);
            }
        }

        private void InitializePropertyTemplates(Type processorPropertyType)
        {
            string propertyAssembly = processorPropertyType.Assembly.GetName().Name!;

            if (processorPropertyType.TryGetCustomAttribute<PropertyTemplateAttribute>(out var attribute))
            {
                string templatePath = attribute.PropertyTemplatePath;
                string resourcePath = $"pack://application:,,,/{propertyAssembly};component/{templatePath}";

                var templateResource = new ResourceDictionary
                {
                    Source = new Uri(resourcePath)
                };

                Application.Current.Resources.MergedDictionaries.Add(templateResource);
            }
        }
    }
}
