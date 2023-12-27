using System.Windows.Input;
using WREdit.Base.Plugins;
using WREdit.Base.Processing;

namespace WREdit.ViewModels
{
    internal class ProcessorsPaneViewModel : ViewModelBase
    {
        private readonly IPluginManager _pluginManager;

        public ICommand ExecuteCommand { get; }

        public ProcessorsPaneViewModel(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
            _pluginManager.InitializePlugins();

            ExecuteCommand = new RelayCommand(
                execute: () => SelectedProcessor?.Execute(null),
                canExecute: () => SelectedProcessor is not null
            );
        }

        public IEnumerable<Type> Processors
        {
            get => _pluginManager.Processors;
        }

        private IEntityProcessor? _selectedProcessor;
        public IEntityProcessor? SelectedProcessor
        {
            get => _selectedProcessor;
            set
            {
                _selectedProcessor = value;
                OnPropertyChanged();

                if (value is not null)
                {
                    ProcessorProperties = new ProcessorPropertiesViewModel(value, _pluginManager);
                }
            }
        }

        private ProcessorPropertiesViewModel? _processorProperties;
        public ProcessorPropertiesViewModel? ProcessorProperties
        {
            get => _processorProperties;
            set
            {
                _processorProperties = value;
                OnPropertyChanged();
            }
        }
    }
}
