using WREdit.Base.Models;

namespace WREdit.DataAccess
{
    internal interface IGameObjectLoader
    {
        GameObject Load(string path);
    }
}