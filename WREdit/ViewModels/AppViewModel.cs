using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WREdit.Base.Plugins;
using WREdit.Base.Processing.Execution;

namespace WREdit.ViewModels
{
    internal partial class AppViewModel : ObservableObject
    {
        private readonly ExecutionStack _executionStack = new();

        public AppViewModel()
        {
            EntitiesListing = new EntityListingViewModel();

            ProcessorSettings = new ProcessorsPaneViewModel(new PluginManager("Plugins"));
            ProcessorSettings.PropertyChanged += (_, _) =>
            {
                ExecuteProcessorCommand.NotifyCanExecuteChanged();
            };

            ExecuteProcessorCommand.PropertyChanged += (_, _) =>
            {
                UndoCommand.NotifyCanExecuteChanged();
            };
        }

        [ObservableProperty]
        private float _processingProgress;

        [ObservableProperty]
        private bool _isProgressIndeterminate;

        [ObservableProperty]
        private string _processedFile;

        [ObservableProperty]
        private EntityListingViewModel _entitiesListing;

        [ObservableProperty]
        private ProcessorsPaneViewModel _processorSettings;

        [RelayCommand(CanExecute = nameof(CanExecuteProcessor))]
        private async Task ExecuteProcessor(CancellationToken token)
        {
            if (ProcessorSettings.SelectedProcessor is null)
                return;

            var processor = ProcessorSettings.SelectedProcessor!;
            var entities = EntitiesListing.Entities.Select(e => e.Entity);
            var progress = new Progress<ProgressReport>((report) =>
            {
                ProcessedFile = report.CurrentFile;
                ProcessingProgress = report.Progress;
            });

            var execution = new ProcessExecution(processor, entities);
            await _executionStack.Execute(execution, progress);
        }

        private bool CanExecuteProcessor()
        {
            return ProcessorSettings?.SelectedProcessor is not null;
        }

        [RelayCommand(CanExecute = nameof(CanUndo))]
        private async Task Undo()
        {
            if (_executionStack.CanUndo)
            {
                var progress = new Progress<ProgressReport>((report) =>
                {
                    ProcessedFile = report.CurrentFile;
                    ProcessingProgress = report.Progress;
                });

                await _executionStack.Undo(progress);

                foreach (var item in EntitiesListing.Entities)
                {
                    item.Entity.Load();
                }
            }
        }

        private bool CanUndo()
        {
            return !ExecuteProcessorCommand.IsRunning && _executionStack.CanUndo;
        }
    }
}
