using WREdit.Base.Models;
using WREdit.Base.Models.Properties;

namespace WREdit.ViewModels
{
    internal class ProcessorPropertiesViewModel : ViewModelBase
    {
        private readonly IGameObjectProcessor _processor;

        public ProcessorPropertiesViewModel(IGameObjectProcessor processor)
        {
            _processor = processor;
        }
    }
}
