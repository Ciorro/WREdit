using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using WREdit.Base.Entities;

namespace WREdit.ViewModels
{
    internal class EntityItemViewModel : ObservableObject
    {
        public IEntity Entity { get; }

        public bool IsSelected { get; set; }

        public EntityItemViewModel(string entityPath)
        {
            Entity = new Entity(entityPath);
            Entity.Load();
        }

        public string EntityPath
        {
            get => Entity.FileName;
        }

        private string? _name;
        public string Name
        {
            get => _name ??= FindName();
        }

        private string? _icon;
        public string? Icon
        {
            get => _icon ??= FindIconPath();
        }

        private string FindName()
        {
            var nameProperty = Entity.SelectNextProperty("$NAME_STR string:name");
            if (nameProperty is null)
            {
                nameProperty = Entity.SelectNextProperty("$NAME number:name");
            }

            return nameProperty?["name"]?.ToString() ?? "Unknown";
        }

        private string? FindIconPath()
        {
            var entityName = Path.GetFileNameWithoutExtension(EntityPath);
            var entityDirectory = Path.GetDirectoryName(EntityPath);
            var directoryInfo = new DirectoryInfo(entityDirectory!);

            if (directoryInfo.Parent?.Name == "media_soviet")
            {
                return Path.Combine(directoryInfo.Parent.FullName, "editor", $"tool_{entityName}.png");
            }

            if (directoryInfo.GetFiles().Any(f => f.Name == "imagegui.png"))
            {
                return Path.Combine(directoryInfo.FullName, "imagegui.png");
            }

            if (directoryInfo.Parent?.GetFiles().Any(f => f.Name == "imagegui.png") == true)
            {
                return Path.Combine(directoryInfo.Parent.FullName, "imagegui.png");
            }

            return null;
        }
    }
}
