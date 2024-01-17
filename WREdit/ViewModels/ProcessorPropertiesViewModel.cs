using System.Collections.ObjectModel;
using System.Reflection;
using WREdit.Base.Attributes;
using WREdit.Base.Extensions;
using WREdit.Base.Plugins;
using WREdit.Base.Processing;
using WREdit.Base.Processing.Properties;

namespace WREdit.ViewModels
{
    internal class ProcessorPropertiesViewModel
    {
        public ObservableCollection<IProcessorProperty> Properties { get; } = new();

        public ProcessorPropertiesViewModel(IEntityProcessor processor, IPluginManager pluginManager)
        {
            processor.RegisterProperties(Properties);
            RegisterAutoProperties(processor, pluginManager);
        }

        private void RegisterAutoProperties(IEntityProcessor processor, IPluginManager pluginManager)
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
            propertyType = (Nullable.GetUnderlyingType(propertyType) ?? propertyType);

            //Find exact handler type
            foreach (var handlerType in pluginManager.Properties)
            {
                var targetTypesAttribute = handlerType.GetCustomAttributes<TargetTypeAttribute>();
                var targetTypes = targetTypesAttribute.Select(attr => attr.TargetType);

                if (targetTypes.Contains(propertyType))
                {
                    return handlerType;
                }
            }

            //Find closest handler type
            foreach (var handlerType in pluginManager.Properties)
            {
                var targetTypesAttribute = handlerType.GetCustomAttributes<TargetTypeAttribute>();
                var targetTypes = targetTypesAttribute.Select(attr => attr.TargetType);

                if (targetTypes.Any(type => type.IsAssignableFrom(propertyType)))
                {
                    return handlerType;
                }
            }

            return null;
        }
    }
}
