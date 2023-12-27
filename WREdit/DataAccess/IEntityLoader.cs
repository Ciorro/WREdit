using WREdit.Base.Models;

namespace WREdit.DataAccess
{
    internal interface IEntityLoader
    {
        Entity Load(string path);
    }
}