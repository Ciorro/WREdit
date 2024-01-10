using WREdit.Base.Entities;
using WREdit.ViewModels;

namespace WREdit.DataAccess
{
    internal interface IEntityLoader
    {
        EntityItemViewModel Load(string path);
    }
}