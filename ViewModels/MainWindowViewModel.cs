using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly List<Class> _workingList;

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
            _workingList = new List<Class>()
            {
                new()
                {
                    ClassData = new ClassData('A', "II", false),
                    Points = 1000
                },
                new()
                {
                    ClassData = new ClassData('A', "II", false),
                    Points = 500
                },
                new()
                {
                    ClassData = new ClassData('A', "II", false),
                    Points = 0
                }
            };
            UpdatePlaces();
            AddClassCommand = new RelayCommand(() =>
            {
                ManageClass manageClassWindow = new();
                ManageClassViewModel manageClassViewModel = new((c) =>
                {
                    _workingList.Add(c);
                    ReorderClasses();
                    CommitChanges();
                }, () => manageClassWindow.Close());
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            });
            ModifyClass = new RelayCommand(() =>
            {
                var selectedClassIndex = _workingList.IndexOf(SelectedClass!);
                ManageClass manageClassWindow = new();
                var pointsChanged = false;
                ManageClassViewModel manageClassViewModel = new((c) =>
                {
                    if (c.Points != _workingList[selectedClassIndex].Points) pointsChanged = true;
                    _workingList[selectedClassIndex] = c;
                    CommitChanges();
                    if (!pointsChanged) return;
                    ReorderClasses();
                    CommitChanges();
                }, () => manageClassWindow.Close(), SelectedClass);
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            }, () => SelectedClass != null);
            RemoveClassCommand = new RelayCommand(() =>
            {
                _workingList.Remove(SelectedClass!);
                UpdatePlaces();
                CommitChanges();
            }, () => SelectedClass != null);
            
            CommitChanges();
        }

        // Use this instead of sort because of time complexity (this -> O(n), sort -> O(n log n))
        // We need to find correct position only for one newly added element
        // We are assuming that list is sorted before adding new item
        private void ReorderClasses()
        {
            var i = 0;
            var j = _workingList.Count - 1;
            while(i < _workingList.Count - 1)
            {
                if (_workingList[i].Points < _workingList[j].Points)
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
        }

        private void UpdatePlaces()
        {
            var currPlace = 1;
            var prevPoints = _workingList[0].Points;
            _workingList[0].Place = 1;
            for (var i = 1; i < _workingList.Count; i++)
            {
                if (_workingList[i].Points != prevPoints) currPlace++;
                _workingList[i].Place = currPlace;
                prevPoints = _workingList[i].Points;
            }
        }

        private void SwapClasses(int left, int right)
        {
            var temp = _workingList[left];
            _workingList[left] = _workingList[right];
            _workingList[right] = temp;
        }

        // Hacky way to update observable, but it works
        private void CommitChanges()
        {
            Classes = new ObservableCollection<Class>();
            Classes = new ObservableCollection<Class>(_workingList);
        }
    }
}