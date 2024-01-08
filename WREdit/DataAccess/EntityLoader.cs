using System.IO;
using WREdit.Base.Entities;

namespace WREdit.DataAccess
{
    internal class EntityLoader : IEntityLoader
    {
        public Entity Load(string path)
        {
            return new Entity(File.ReadAllText(path));
        }
    }
}
