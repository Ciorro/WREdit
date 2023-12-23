using System.Reflection;
using WREdit.Base.Attributes;
using WREdit.Base.Extensions;

namespace WREdit.Base.Properties
{
    public abstract class ProcessorProperty : IProcessorProperty
    {
        protected readonly object Instance;
        protected readonly PropertyInfo Property;

        public string Name { get; init; }

        public ProcessorProperty(string propertyName, object instance)
        {
            if (!instance.GetType().TryGetProperty(propertyName, out var property))
            {
                throw new ArgumentException($"Property named {propertyName} does not exist in the object of type {instance.GetType()}");
            }

            Property = property;
            Instance = instance;
            Name = Property.Name;

            if (Property.TryGetCustomAttribute<PropertyAttribute>(out var attribute))
            {
                Name = attribute.DisplayName!;
            }
        }

        public virtual object? Value
        {
            get => Property.GetValue(Instance);
            set => Property.SetValue(Instance, value);
        }
    }
}
