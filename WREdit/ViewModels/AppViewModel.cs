using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private EntityListingViewModel? _entitiesListing;

        [ObservableProperty]
        private ProcessorsPaneViewModel? _processorSettings;

        [RelayCommand(CanExecute = nameof(CanExecuteProcessor))]
        private void ExecuteProcessor()
        {
            var entities = EntitiesListing?.Entities;

            foreach (var entityItem in entities)
            {
                var entity = entityItem.Entity;

                try
                {
                    ProcessorSettings?.SelectedProcessor?.Execute(entity);
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        e.Message,
                        entityItem.Name,
                        MessageBoxButton.OK,
                        MessageBoxImage.Asterisk
                    );
                }
            }
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
