using WREdit.Base.Models;

namespace WREdit.ViewModels
{
    internal class GameObjectItemViewModel : ViewModelBase
    {
        public GameObject GameObject { get; }
        public bool IsSelected { get; set; }

        public GameObjectItemViewModel(GameObject gameObject)
        {
            GameObject = gameObject;

            //var nameProperty = GameObject.Properties
            //    .Select(p => p as NameStrProperty)
            //    .FirstOrDefault();

            //_name = nameProperty?.ObjectName ?? "???";
        }

        private string _name = "";
        public string Name
        {
            get => _name;
        }

        private string _icon = "";
        public string Icon
        {
            get => _icon;
            init
            {
                _icon = value;
                OnPropertyChanged();
            }
        }
    }
}
