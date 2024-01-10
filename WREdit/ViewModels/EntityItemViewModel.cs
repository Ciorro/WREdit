using WREdit.Base.Entities;

namespace WREdit.ViewModels
{
    internal class EntityItemViewModel : ViewModelBase
    {
        public IEntity Entity { get; }
        public bool IsSelected { get; set; }

        public EntityItemViewModel(IEntity entity)
        {
            Entity = entity;

            var nameStrProperty = entity.SelectNextProperty("$NAME_STR string:name");
            _name = nameStrProperty?.GetValue<string>("name") ?? "Unknown";
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
