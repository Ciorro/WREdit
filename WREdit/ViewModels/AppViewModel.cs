using WREdit.DataAccess;
using WREdit.Base.Plugins;
using WREdit.Base.Processing;
using WREdit.Base.Entities;

namespace WREdit.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            EntitiesListing = new EntityListingViewModel(new EntityLoader());
            ActionSettings = new ProcessorsPaneViewModel(new PluginManager("Plugins"));
            ActionSettings.ProcessorExecuted += OnProcessorExecuted;
        }

        private EntityListingViewModel? _entitiesListing;
        public EntityListingViewModel? EntitiesListing
        {
            get => _entitiesListing;
            set
            {
                _entitiesListing = value;
                OnPropertyChanged();
            }
        }

        private ProcessorsPaneViewModel? _actionSettings;
        public ProcessorsPaneViewModel? ActionSettings
        {
            get => _actionSettings;
            set
            {
                _actionSettings = value;
                OnPropertyChanged();
            }
        }

        private void OnProcessorExecuted(IEntityProcessor processor)
        {
            var entities = EntitiesListing?.Entities?.Select(e => e.Entity);

            foreach (var entity in entities ?? Enumerable.Empty<Entity>())
            {
                processor.Execute(entity);
            }
        }
    }
}
