using WREdit.DataAccess;

namespace WREdit.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            EntitiesListing = new GameObjectListingViewModel(new GameObjectLoader());
            ActionSettings = new ActionsViewModel();
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

        private ActionsViewModel? _actionSettings;
        public ActionsViewModel? ActionSettings
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
