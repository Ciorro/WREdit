using WREdit.Base.Properties;

namespace WREdit.Base.Models
{
    public interface IGameObjectProcessor
    {
        void Execute(GameObject gameObject);
        void RegisterProperties(ICollection<IProcessorProperty> properties);
    }
}