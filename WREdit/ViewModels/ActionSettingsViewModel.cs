using System.Collections.ObjectModel;
using System.Windows.Input;
using WREdit.Base.Actions;
using WREdit.DataAccess;
using WREdit.Plugins;

namespace WREdit.ViewModels
{
    internal class ActionSettingsViewModel : ViewModelBase
    {
        private readonly IPluginManager _pluginManager;
        public event Action<IGameObjectAction>? Executed;
        public ICommand ExecuteCommand { get; }

        public ActionSettingsViewModel(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
            _pluginManager.InitializePlugins();

            ExecuteCommand = new RelayCommand(
                execute: () => Executed?.Invoke(SelectedAction!),
                canExecute: () => SelectedAction is not null
            );

            ActionProperties = new ObservableCollection<ActionPropertyViewModel>();
        }

        public IEnumerable<IGameObjectAction> Actions
        {
            get => _pluginManager.Actions;
        }

        private IGameObjectAction? _selectedAction;
        public IGameObjectAction? SelectedAction
        {
            get => _selectedAction;
            set
            {
                _selectedAction = value;
                OnPropertyChanged();

                if (value is not null)
                {
                    SetProperties(value);
                }
            }
        }

        private ObservableCollection<ActionPropertyViewModel>? _actionProperties;
        public ObservableCollection<ActionPropertyViewModel>? ActionProperties
        {
            get => _actionProperties;
            set
            {
                _actionProperties = value;
                OnPropertyChanged();
            }
        }

        private void SetProperties(IGameObjectAction action)
        {
            Type type = action.GetType();

            foreach (var property in type.GetProperties())
            {
                ActionProperties?.Add(new ActionPropertyViewModel(property, action));
            }
        }
    }
}
