using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WREdit.DataAccess;

namespace WREdit.ViewModels
{
    internal partial class EntityListingViewModel : ObservableObject
    {
        private readonly IEntityLoader _loader;
        public ObservableCollection<EntityItemViewModel> Entities { get; } = new();

        public EntityListingViewModel(IEntityLoader loader)
        {
            _loader = loader;
        }

        [RelayCommand]
        private void AddEntity()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "*.ini (Ini files)|*.ini",
                Multiselect = true,
                //TODO: Remove:
                InitialDirectory = "E:\\Gry\\steamapps\\workshop\\content\\784150"
            };

            if (fileDialog.ShowDialog() == true)
            {
                //TODO: Validate the path (the path has to point inside the game folder or workshop folder)
                foreach (var file in fileDialog.FileNames)
                {
                    Entities.Add(_loader.Load(file));
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanRemoveEntity))]
        private void RemoveEntity()
        {
            var toRemove = Entities.Where(go => go.IsSelected).ToArray();

            foreach (var obj in toRemove)
            {
                Entities.Remove(obj);
            }
        }

        private bool CanRemoveEntity()
        {
            return Entities.Any(p => p.IsSelected);
        }

        [RelayCommand]
        private void SelectionChanged()
        {
            RemoveEntityCommand.NotifyCanExecuteChanged();
        }
    }
}
