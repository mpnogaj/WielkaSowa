using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WielkaSowa.Helpers;
using WielkaSowa.Models;
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

        public MainWindowViewModel()
        {
            _classes = new ObservableCollection<Class>();
            AddClassCommand = new RelayCommand(() =>
            {
                ManageClass manageClassWindow = new();
                ManageClassViewModel manageClassViewModel = new((c) =>
                {
                    ReorderAndUpdateUI();
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
                        UpdateUI();
                    }
                    else
                    {
                        ReorderAndUpdateUI();
                    }
                }, () => manageClassWindow.Close(), SelectedClass);
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            }, () => SelectedClass != null);
            RemoveClassCommand = new RelayCommand(() =>
            {
                Storage.Instance!.Classes.Remove(SelectedClass!);
                Storage.Instance!.UpdateCalcs();
                ReorderAndUpdateUI();
            }, () => SelectedClass != null);
        }

        // Use this instead of sort because of time complexity (this -> O(n), sort -> O(n log n))
        // We need to find correct position only for one newly added element
        // We are assuming that list is sorted before adding new item
        private void ReorderAndUpdateUI()
        {
            try
            {
                var i = 0;
                var j = Storage.Instance!.Classes.Count - 1;
                while (i < Storage.Instance!.Classes.Count - 1)
                {
                    if (Storage.Instance!.Classes[i].Points < Storage.Instance!.Classes[j].Points)
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

                UpdatePlaces();
                UpdateUI();
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
                Storage.Instance!.Classes[0].Place = 1;
                for (var i = 1; i < Storage.Instance!.Classes.Count; i++)
                {
                    if (Storage.Instance!.Classes[i].Points != prevPoints) currPlace++;
                    Storage.Instance!.Classes[i].Place = currPlace;
                    prevPoints = Storage.Instance!.Classes[i].Points;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                /*No active classes. Just ignore*/
            }
        }

        private void SwapClasses(int left, int right)
        {
            var temp = Storage.Instance!.Classes[left];
            Storage.Instance!.Classes[left] = Storage.Instance!.Classes[right];
            Storage.Instance!.Classes[right] = temp;
        }

        // Hacky way to update observable, but it works
        private void UpdateUI()
        {
            Debug.WriteLine("Updating UI. New UI: ");
            foreach(Class c in Storage.Instance!.Classes)
            {
                Debug.WriteLine(c.ToString());
            }
            Debug.WriteLine("========");
            Classes = new ObservableCollection<Class>();
            Classes = new ObservableCollection<Class>(Storage.Instance!.Classes);
        }
    }
}