namespace WREdit.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            EntitiesListing = new EntitiesListingViewModel();
            ActionSettings = new ActionSettingsViewModel();
        }

        private EntitiesListingViewModel? _entitiesListing;
        public EntitiesListingViewModel? EntitiesListing
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
