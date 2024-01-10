using System.IO;
using WREdit.Base.Entities;

namespace WREdit.DataAccess
{
    internal class EntityLoader : IEntityLoader
    {
        public IEntity Load(string path)
        {
            return new Entity(File.ReadAllText(path));
        }
    }
}
