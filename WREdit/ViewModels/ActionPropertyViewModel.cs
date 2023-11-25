using System.Reflection;
using WREdit.Base.Actions;
using WREdit.Base.Attributes;

namespace WREdit.ViewModels
{
    internal class ActionPropertyViewModel : ViewModelBase
    {
        private readonly PropertyInfo _property;
        private readonly IGameObjectAction _actionInstance;

        public ActionPropertyViewModel(PropertyInfo property, IGameObjectAction actionInstance)
        {
            _property = property;
            _actionInstance = actionInstance;
        }

        public string Name
        {
            get => _property.GetCustomAttribute<GameObjectActionPropertyAttribute>()?.DisplayName ?? _property.Name;
        }

        public object? Value
        {
            get
            {
                return _property.GetValue(_actionInstance);
            }
            set
            {
                _property.SetValue(_actionInstance, value);
                OnPropertyChanged();
            }
        }
    }
}
