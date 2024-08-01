using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using WREdit.Base.Entities;
using WREdit.Base.Translation;

namespace WREdit.ViewModels
{
    internal class EntityItemViewModel : ObservableObject
    {
        private readonly ITranslationProvider _translationProvider;

        public IEntity Entity { get; }
        public bool IsSelected { get; set; }

        public EntityItemViewModel(IEntity entity, ITranslationProvider translationProvider)
        {
            _translationProvider = translationProvider;
            Entity = entity;
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
            if (Entity.TrySelectNextProperty("$NAME_STR string:name", out var nameStr))
            {
                return nameStr.GetValue<string>("name");
            }

            if (Entity.TrySelectNextProperty("$NAME number:name", out var nameNum))
            {
                int nameId = nameNum.GetValue<int>("name");
                return _translationProvider.GetString(nameId);
            }

            return "Unknown";
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
