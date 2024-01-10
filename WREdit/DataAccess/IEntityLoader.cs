using WREdit.Base.Entities;

namespace WREdit.DataAccess
{
    internal interface IEntityLoader
    {
        IEntity Load(string path);
    }
}