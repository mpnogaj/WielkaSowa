using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using WielkaSowa.Helpers;
using WielkaSowa.Models;
using WielkaSowa.Services;
using WielkaSowa.ViewModels.Commands;
using WielkaSowa.ViewModels.Commands.Async;
using WielkaSowa.Views;

namespace WielkaSowa.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Class> _classes;
        public ObservableCollection<Class> Classes
        {
            get => _classes;
            set => SetProperty(ref _classes, value);
        }

        private Class? _selectedClass;
        public Class? SelectedClass
        {
            get => _selectedClass;
            set => SetProperty(ref _selectedClass, value);
        }

        public RelayCommand AddClassCommand { get; }
        public RelayCommand ModifyClass { get; }
        public RelayCommand RemoveClassCommand { get; }
        public RelayCommand OpenSettingsCommand { get; }
        public AsyncRelayCommand OpenFileCommand { get; }
        public AsyncRelayCommand SaveCommand { get; }
        public AsyncRelayCommand SaveAsCommand { get; }
        public RelayCommand OpenHelp { get; }
        public RelayCommand OpenIssues { get; }
        public RelayCommand OpenContact { get; }

        public MainWindowViewModel()
        {
            _classes = new();
            Storage.Instance!.ClassesUpdated += (_, _) => UpdateUi(); 
            AddClassCommand = new RelayCommand(() =>
            {
                ManageClass manageClassWindow = new();
                ManageClassViewModel manageClassViewModel = new((_) =>
                {
                    ReorderAndUpdateUi();
                }, () => manageClassWindow.Close());
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            });
            ModifyClass = new RelayCommand(() =>
            {
                ManageClass manageClassWindow = new();
                var prevPoints = SelectedClass!.Points;
                ManageClassViewModel manageClassViewModel = new((c) =>
                {
                    if (prevPoints == c.Points)
                    {
                        UpdateUi();
                    }
                    else
                    {
                        ReorderAndUpdateUi();
                    }
                }, () => manageClassWindow.Close(), SelectedClass);
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            }, () => SelectedClass != null);
            RemoveClassCommand = new RelayCommand(() =>
            {
                Storage.Instance.Classes.Remove(SelectedClass!);
                Storage.UpdateCalcs();
                ReorderAndUpdateUi();
            }, () => SelectedClass != null);
			OpenSettingsCommand = new RelayCommand(() => 
			{
				SettingsWindow settingsWindow = new();
				settingsWindow.ShowDialog(Essentials.GetMainWindow());
			});
            OpenFileCommand = new AsyncRelayCommand(async () =>
            {
                OpenFileDialog dialog = new()
                {
                    AllowMultiple = false,
                    Title = "Otw???rz plik z danymi",
                    Filters = Constants.DataFileFilters
                };
                var res = await dialog.ShowAsync(Essentials.GetMainWindow());
                if (res != null && res.Length > 0 && !string.IsNullOrEmpty(res[0]))
                {
                    await Storage.Instance.OpenAndLoadFile(res[0]);
                }
            });
            SaveCommand = new AsyncRelayCommand(async () =>
            {
                if (File.Exists(Storage.Instance.CurrentFilePath))
                {
                    await Storage.Instance.SaveToFile();
                }
                else
                {
                    await SaveAs();
                }
            });
            SaveAsCommand = new AsyncRelayCommand(async () => 
            {
                await SaveAs();
            });
            OpenHelp = new RelayCommand(() => Essentials.OpenUrl("https://github.com/mpnogaj/WielkaSowa/wiki"));
            OpenIssues = new RelayCommand(() => Essentials.OpenUrl("https://github.com/mpnogaj/WielkaSowa/issues"));
            OpenContact = new RelayCommand(() => new ContactWindow().ShowDialog(Essentials.GetMainWindow()));
        }

        private async Task SaveAs()
        {
            SaveFileDialog dialog = new()
            {
                Filters = Constants.DataFileFilters,
                Title = "Zapisz dane do pliku"
            };
            string? res = await dialog.ShowAsync(Essentials.GetMainWindow());
            if (!string.IsNullOrEmpty(res))
            {
                await Storage.Instance.SaveToFile(res);
            }
        }

        // Use this instead of sort because of time complexity (this -> O(n), sort -> O(n log n))
        // We need to find correct position only for one newly added element
        // We are assuming that list is sorted before adding new item
        private void ReorderAndUpdateUi()
        {
            try
            {
                var i = 0;
                var j = Storage.Instance!.Classes.Count - 1;
                while (i < Storage.Instance.Classes.Count - 1)
                {
                    if (Storage.Instance.Classes[i].Points < Storage.Instance.Classes[j].Points)
                    {
                        SwapClasses(i, j);
                        break;
                    }

                    i++;
                }

                // i -> new item correct position
                // j -> end of the list
                while (j - 1 > i)
                {
                    SwapClasses(j, j - 1);
                    j--;
                }

                UpdateUi();
            }
            catch (ArgumentOutOfRangeException)
            {
                /*No active classes. Just ignore*/
            }
        }

        private void UpdatePlaces()
        {
            try
            {
                var currPlace = 1;
                var prevPoints = Storage.Instance!.Classes[0].Points;
                Storage.Instance.Classes[0].Place = 1;
                for (var i = 1; i < Storage.Instance.Classes.Count; i++)
                {
                    if (Storage.Instance.Classes[i].Points != prevPoints) currPlace++;
                    Storage.Instance.Classes[i].Place = currPlace;
                    prevPoints = Storage.Instance.Classes[i].Points;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                /*No active classes. Just ignore*/
            }
        }

        private void SwapClasses(int left, int right)
        {
            (Storage.Instance!.Classes[left], Storage.Instance.Classes[right]) = (Storage.Instance.Classes[right], Storage.Instance.Classes[left]);
        }

        // Hacky way to update observable, but it works
        private void UpdateUi()
        {
            UpdatePlaces();
            Debug.WriteLine("Updating UI. New UI: ");
            foreach(Class c in Storage.Instance!.Classes)
            {
                Debug.WriteLine(c.ToString());
            }
            Debug.WriteLine("========");
            Classes = new ObservableCollection<Class>();
            Classes = new ObservableCollection<Class>(Storage.Instance.Classes);
        }
    }
}
