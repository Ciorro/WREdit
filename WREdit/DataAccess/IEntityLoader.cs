using WREdit.Base.Entities;

namespace WREdit.DataAccess
{
    internal interface IEntityLoader
    {
        Entity Load(string path);
    }
}