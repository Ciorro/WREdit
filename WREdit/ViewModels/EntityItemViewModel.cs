using System.IO;
using WREdit.Base.Entities;

namespace WREdit.ViewModels
{
    internal class EntityItemViewModel : ViewModelBase
    {
        public IEntity Entity { get; }
        public string EntityPath { get; }
        public string? IconPath { get; }

        public bool IsSelected { get; set; }

        public EntityItemViewModel(string entityPath, string? iconPath = default)
        {
            EntityPath = entityPath;
            IconPath = iconPath;

            Entity = new Entity(File.ReadAllText(entityPath));
            _name = GetName();
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

        private string GetName()
        {
            var nameProperty = Entity.SelectNextProperty("$NAME_STR string:name");
            if (nameProperty is null)
            {
                nameProperty = Entity.SelectNextProperty("$NAME number:name");
            }

            return nameProperty?["name"]?.ToString() ?? "Unknown";
        }
    }
}
