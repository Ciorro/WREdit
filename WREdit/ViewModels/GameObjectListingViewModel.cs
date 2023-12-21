using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WREdit.DataAccess;

namespace WREdit.ViewModels
{
    internal class GameObjectListingViewModel : ViewModelBase
    {
        private readonly IGameObjectLoader _loader;

        public ObservableCollection<GameObjectItemViewModel> GameObjects { get; } = new();
        public ICommand AddObjectCommand { get; }
        public ICommand RemoveObjectCommand { get; }

        public GameObjectListingViewModel(IGameObjectLoader loader)
        {
            _loader = loader;

            AddObjectCommand = new RelayCommand(Add);
            RemoveObjectCommand = new RelayCommand(Remove, CanRemove);
        }

        private void Add()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "*.ini (Ini files)|*.ini",
                //TODO: Remove:
                InitialDirectory = "E:\\Gry\\steamapps\\workshop\\content\\784150"
            };

            if (fileDialog.ShowDialog() == true)
            {
                //TODO: Validate the path (the path has to point inside the game folder or workshop folder)
                var gameObject = _loader.Load(fileDialog.FileName);
                GameObjects.Add(new GameObjectItemViewModel(gameObject));
            }
        }

        private void Remove()
        {
            var toRemove = GameObjects.Where(go => go.IsSelected).ToArray();

            foreach (var obj in toRemove)
            {
                GameObjects.Remove(obj);
            }
        }

        private bool CanRemove()
        {
            return GameObjects.Any(p => p.IsSelected);
        }
    }
}
