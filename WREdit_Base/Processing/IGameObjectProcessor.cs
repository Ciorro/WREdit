using WREdit.Base.Models;
using WREdit.Base.Properties;

namespace WREdit.Base.Processing
{
    public interface IGameObjectProcessor
    {
        void Execute(GameObject gameObject);
        void RegisterProperties(ICollection<IProcessorProperty> properties);
    }
}