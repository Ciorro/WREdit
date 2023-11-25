using WREdit.DataAccess;
using WREdit.Plugins;

namespace WREdit.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            EntitiesListing = new GameObjectListingViewModel(new GameObjectLoader());
            ActionSettings = new ActionSettingsViewModel(new PluginManager("Actions"));
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

        private ActionSettingsViewModel? _actionSettings;
        public ActionSettingsViewModel? ActionSettings
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
