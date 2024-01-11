using CommunityToolkit.Mvvm.ComponentModel;
using WREdit.Base.Plugins;
using WREdit.Base.Processing;

namespace WREdit.ViewModels
{
    internal partial class ProcessorsPaneViewModel : ObservableObject
    {
        private readonly IPluginManager _pluginManager;

        public ProcessorsPaneViewModel(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
            _pluginManager.InitializePlugins();
        }

        public IEnumerable<Type> Processors
        {
            get => _pluginManager.Processors;
        }

        [ObservableProperty]
        private IEntityProcessor? _selectedProcessor;

        [ObservableProperty]
        private ProcessorPropertiesViewModel? _processorProperties;

        partial void OnSelectedProcessorChanged(IEntityProcessor? value)
        {
            if (value is not null)
            {
                ProcessorProperties = new ProcessorPropertiesViewModel(value, _pluginManager);
            }
        }
    }
}
