using WREdit.DataAccess;
using WREdit.Plugins;

namespace WREdit.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            EntitiesListing = new GameObjectListingViewModel(new GameObjectLoader());
            ActionSettings = new ProcessorsPaneViewModel(new PluginManager("Plugins"));
        }

        private GameObjectListingViewModel? _entitiesListing;
        public GameObjectListingViewModel? EntitiesListing
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
