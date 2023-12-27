using WREdit.DataAccess;
using WREdit.Base.Plugins;

namespace WREdit.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            EntitiesListing = new EntityListingViewModel(new EntityLoader());
            ActionSettings = new ProcessorsPaneViewModel(new PluginManager("Plugins"));
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
    }
}
