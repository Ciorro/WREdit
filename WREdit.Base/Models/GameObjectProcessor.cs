using WREdit.Base.Models.Properties;
using WREdit.Base.Extensions;
using WREdit.Base.Attributes;

namespace WREdit.Base.Models
{
    public abstract class GameObjectProcessor : IGameObjectProcessor
    {
        public abstract void Execute(GameObject gameObject);

        public virtual void RegisterProperties(ICollection<IProcessorProperty> properties)
        {
            foreach (var property in GetType().GetProperties())
            {
                if (property.TryGetCustomAttribute<PropertyAttribute>(out var attribute))
                {
                    string name = attribute.DisplayName ?? property.Name;
                    properties.Add(new IntegerProperty(name, 10));
                }
            }
        }
    }
}
