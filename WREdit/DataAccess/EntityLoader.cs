using System.IO;
using WREdit.ViewModels;

namespace WREdit.DataAccess
{
    internal class EntityLoader : IEntityLoader
    {
        public EntityItemViewModel Load(string path)
        {
            return new EntityItemViewModel(path, FindIconPath(path));
        }

        private string? FindIconPath(string entityPath)
        {
            var entityName = Path.GetFileNameWithoutExtension(entityPath);
            var entityDirectory = Path.GetDirectoryName(entityPath);
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
