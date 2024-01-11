using CommunityToolkit.Mvvm.ComponentModel;
using WREdit.Base.Entities;

namespace WREdit.ViewModels
{
    internal class EntityItemViewModel : ObservableObject
    {
        public IEntity Entity { get; }
        public string? IconPath { get; }
        public string? Icon { get; }
        public string Name { get; }

        public bool IsSelected { get; set; }

        public EntityItemViewModel(string entityPath, string? iconPath = default)
        {
            Entity = Base.Entities.Entity.FromFile(entityPath);
            Icon = iconPath;
            Name = GetName();
        }

        public string EntityPath
        {
            get => Entity.FileName;
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
