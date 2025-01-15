using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using WREdit.Base.Entities;
using WREdit.Base.Translation;

namespace WREdit.ViewModels
{
    internal partial class EntityListingViewModel : ObservableObject
    {
        private readonly ITranslationProvider _translationProvider;
        private readonly ObservableCollection<EntityItemViewModel> _entities = new();
        private readonly Regex _filterRegex;

        [ObservableProperty]
        private ICollectionView? _entitiesView;

        public EntityListingViewModel(ITranslationProvider translationProvider)
        {
            _translationProvider = translationProvider;
            _filterRegex = new Regex("^\\${1}(?'verb'[^\\s]+)\\s(?'args'.+)");
            EntitiesView = CollectionViewSource.GetDefaultView(_entities);
        }

        public IEnumerable<IEntity> Entities
        {
            get => _entities.Select(e => e.Entity);
        }

        public string EntityFilter
        {
            set
            {
                if (_filterRegex.IsMatch(value))
                {
                    var match = _filterRegex.Match(value);

                    EntitiesView!.Filter = GetParametrizedEntityFilter(
                        verb: match.Groups["verb"].ToString(),
                        args: match.Groups["args"].ToString()
                    );
                }
                else
                {
                    EntitiesView!.Filter = GetBasicEntityFilter(value ?? "");
                }
            }
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
            var toRemove = _entities.Where(go => go.IsSelected).ToArray();

            foreach (var obj in toRemove)
            {
                _entities.Remove(obj);
            }
        }

        private bool CanRemoveEntity()
        {
            return _entities.Any(p => p.IsSelected);
        }

        [RelayCommand]
        private void InvertSelection()
        {
            foreach (var entity in _entities)
            {
                entity.IsSelected = !entity.IsSelected;
            }
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

                    _entities.Add(new EntityItemViewModel(entityScript, _translationProvider));
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

        private Predicate<object> GetBasicEntityFilter(string filter)
        {
            return (obj) =>
            {
                if (obj is EntityItemViewModel entity)
                {
                    return entity.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
                }
                return false;
            };
        }

        private Predicate<object> GetParametrizedEntityFilter(string verb, string args)
        {
            return (obj) =>
            {
                if (obj is EntityItemViewModel entity)
                {
                    switch(verb.ToLower())
                    {
                        case "type":
                            return entity.Entity.TrySelectNextProperty(args, out _);
                    }
                }
                return false;
            };
        }
    }
}
