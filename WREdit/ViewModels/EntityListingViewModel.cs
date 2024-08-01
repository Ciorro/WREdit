using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WREdit.Base.Entities;
using WREdit.Base.Translation;

namespace WREdit.ViewModels
{
    internal partial class EntityListingViewModel : ObservableObject
    {
        private readonly ITranslationProvider _translationProvider;
        public ObservableCollection<EntityItemViewModel> Entities { get; } = new();

        public EntityListingViewModel(ITranslationProvider translationProvider)
        {
            _translationProvider = translationProvider;
        }

        [RelayCommand]
        private void AddEntity()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "*.ini (Ini files)|*.ini",
                Multiselect = true
            };

            if (fileDialog.ShowDialog() == true)
            {
                LoadFiles(fileDialog.FileNames);
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

        private void LoadFiles(IEnumerable<string> files)
        {
            var errors = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    var entityScript = new Entity(file);
                    entityScript.Load();

                    Entities.Add(new EntityItemViewModel(entityScript, _translationProvider));
                }
                catch (InvalidDataException)
                {
                    errors.Add(file);
                }
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(
                    $"{errors.Count} errors occurred during loading. The following files were not loaded:\n{string.Join('\n', errors)}",
                    "Loading error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }
    }
}
