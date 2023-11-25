using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WREdit.DataAccess;

namespace WREdit.ViewModels
{
    internal class GameObjectListingViewModel : ViewModelBase
    {
        private readonly IGameObjectLoader _loader;

        public ObservableCollection<GameObjectViewModel> GameObjects { get; } = new();
        public ICommand AddObjectCommand { get; }
        public ICommand RemoveObjectCommand { get; }

        public GameObjectListingViewModel(IGameObjectLoader loader)
        {
            _loader = loader;

            AddObjectCommand = new RelayCommand(AddObject);
            RemoveObjectCommand = new RelayCommand(RemoveObject, CanRemoveObject);
        }

        private void AddObject()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "*.ini (Ini files)|*.ini",
                //TODO: Remove:
                InitialDirectory = "E:\\Gry\\steamapps\\workshop\\content\\784150"
            };

            if (fileDialog.ShowDialog() == true)
            {
                var gameObject = _loader.Load(fileDialog.FileName);
                GameObjects.Add(new GameObjectViewModel(gameObject));
            }
        }

        private void RemoveObject()
        {
            var toRemove = GameObjects.Where(go => go.IsSelected).ToArray();

            foreach (var obj in toRemove)
            {
                GameObjects.Remove(obj);
            }
        }

        private bool CanRemoveObject()
        {
            return GameObjects.Any(p => p.IsSelected);
        }
    }
}
