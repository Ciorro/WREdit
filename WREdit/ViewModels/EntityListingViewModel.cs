using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WREdit.DataAccess;

namespace WREdit.ViewModels
{
    internal class EntityListingViewModel : ViewModelBase
    {
        private readonly IEntityLoader _loader;

        public ObservableCollection<EntityItemViewModel> Entities { get; } = new();
        public ICommand AddEntityCommand { get; }
        public ICommand RemoveEntityCommand { get; }

        public EntityListingViewModel(IEntityLoader loader)
        {
            _loader = loader;

            AddEntityCommand = new RelayCommand(Add);
            RemoveEntityCommand = new RelayCommand(Remove, CanRemove);
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
                var entity = _loader.Load(fileDialog.FileName);
                Entities.Add(new EntityItemViewModel(entity));
            }
        }

        private void Remove()
        {
            var toRemove = Entities.Where(go => go.IsSelected).ToArray();

            foreach (var obj in toRemove)
            {
                Entities.Remove(obj);
            }
        }

        private bool CanRemove()
        {
            return Entities.Any(p => p.IsSelected);
        }
    }
}
