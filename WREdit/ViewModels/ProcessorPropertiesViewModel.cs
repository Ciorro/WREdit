using System.Collections.ObjectModel;
using System.Reflection;
using WREdit.Base.Attributes;
using WREdit.Base.Extensions;
using WREdit.Base.Plugins;
using WREdit.Base.Processing;
using WREdit.Base.Properties;

namespace WREdit.ViewModels
{
    internal class ProcessorPropertiesViewModel : ViewModelBase
    {
        public ObservableCollection<IProcessorProperty> Properties { get; } = new();

        public ProcessorPropertiesViewModel(IGameObjectProcessor processor, IPluginManager pluginManager)
        {
            processor.RegisterProperties(Properties);
            RegisterAutoProperties(processor, pluginManager);
        }

        private void RegisterAutoProperties(IGameObjectProcessor processor, IPluginManager pluginManager)
        {
            var properties = processor.GetType().GetProperties().Where(p =>
            {
                return p.HasAttribute<PropertyAttribute>();
            });

            foreach (var property in properties)
            {
                var handlerType = ResolvePropertyHandlerType(property.PropertyType, pluginManager);
                if (handlerType is null)
                    continue;

                var handler = (IProcessorProperty?)Activator.CreateInstance(handlerType, property.Name, processor);
                if (handler is null)
                    continue;

                Properties.Add(handler);
            }
        }

        private Type? ResolvePropertyHandlerType(Type propertyType, IPluginManager pluginManager)
        {
            return pluginManager.Properties.Where(p =>
            {
                return p.GetCustomAttributes<TargetTypeAttribute>().Where(t =>
                {
                    return t.TargetType == (Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }).Any();
            }).FirstOrDefault();
        }
    }
}
