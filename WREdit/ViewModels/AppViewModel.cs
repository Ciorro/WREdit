using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows;
using WREdit.Base.Plugins;
using WREdit.DataAccess;

namespace WREdit.ViewModels
{
    internal partial class AppViewModel : ObservableObject
    {
        public AppViewModel()
        {
            EntitiesListing = new EntityListingViewModel(new EntityLoader());
            ProcessorSettings = new ProcessorsPaneViewModel(new PluginManager("Plugins"));
            ProcessorSettings.PropertyChanged += (_, _) =>
            {
                ExecuteProcessorCommand.NotifyCanExecuteChanged();
            };
        }

        [ObservableProperty]
        private float _processingProgress;

        [ObservableProperty]
        private string _processedFile;

        [ObservableProperty]
        private EntityListingViewModel _entitiesListing;

        [ObservableProperty]
        private ProcessorsPaneViewModel _processorSettings;

        [RelayCommand(CanExecute = nameof(CanExecuteProcessor))]
        private async Task ExecuteProcessor(CancellationToken token)
        {
            var entities = EntitiesListing.Entities.Select(e => e.Entity).ToList();

            await Task.Run(() =>
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    ProcessedFile = entities[i].FileName;
                    ProcessorSettings.SelectedProcessor?.Execute(entities[i]);
                    File.WriteAllText(entities[i].FileName, entities[i].Content);
                    ProcessingProgress = (float)i / entities.Count;
                }
            });

            ProcessedFile = "";
            ProcessingProgress = 0;
        }

        private bool CanExecuteProcessor()
        {
            return ProcessorSettings?.SelectedProcessor is not null;
        }

        [RelayCommand(CanExecute = nameof(CanUndo))]
        private void Undo()
        {
            //TODO: Undo
        }

        private bool CanUndo()
        {
            return false;
        }
    }
}
