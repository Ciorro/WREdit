using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using WREdit.Base.Plugins;
using WREdit.Base.Processing.Execution;
using WREdit.Base.Translation;

namespace WREdit.ViewModels
{
    internal partial class AppViewModel : ObservableObject
    {
        private readonly ExecutionStack _executionStack = new();

        public AppViewModel()
        {
            ITranslationProvider translation = File.Exists("language.btf") ?
                new BtfTranslationProvider("language.btf") :
                new DefaultTranslationProvider();

            EntitiesListing = new EntityListingViewModel(translation);

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
        private ProgressReport _progressReport;

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
            var entities = EntitiesListing.Entities;
            var progress = new Progress<ProgressReport>((report) =>
            {
                ProgressReport = report;
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
                    ProgressReport = report;
                });

                await _executionStack.Undo(progress);

                //Reload entities
                foreach (var item in EntitiesListing.Entities)
                {
                    item.Load();
                }
            }
        }

        private bool CanUndo()
        {
            return !ExecuteProcessorCommand.IsRunning && _executionStack.CanUndo;
        }
    }
}
