using WREdit.Base.Actions;

namespace WREdit.Plugins
{
    internal interface IPluginManager
    {
        IEnumerable<IGameObjectAction> Actions { get; }
        void InitializePlugins();
    }
}
