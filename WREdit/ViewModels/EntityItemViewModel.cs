using WREdit.Base.Entities;

namespace WREdit.ViewModels
{
    internal class EntityItemViewModel : ViewModelBase
    {
        public Entity Entity { get; }
        public bool IsSelected { get; set; }

        public EntityItemViewModel(Entity entity)
        {
            Entity = entity;

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
