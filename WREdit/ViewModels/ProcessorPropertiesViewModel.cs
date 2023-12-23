using System.Collections.ObjectModel;
using WREdit.Base.Models;
using WREdit.Base.Properties;

namespace WREdit.ViewModels
{
    internal class ProcessorPropertiesViewModel : ViewModelBase
    {
        public ObservableCollection<IProcessorProperty> Properties { get; } = new();

        public ProcessorPropertiesViewModel(IGameObjectProcessor processor)
        {
            RegisterProperties(processor);
        }

        private void RegisterProperties(IGameObjectProcessor processor)
        {
            processor.RegisterProperties(Properties);
        }
    }
}
