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
            Classes.Add(new()
            {
                ClassData = new('A', "II", false),
                Points = 150
            });
            AddClassCommand = new RelayCommand(() =>
            {
                ManageClass manageClassWindow = new();
                ManageClassViewModel manageClassViewModel = new((c) => Classes.Add(c), () => manageClassWindow.Close());
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            });
            ModifyClass = new RelayCommand(() =>
            {
                var selectedClassIndex = Classes.IndexOf(SelectedClass!);
                ManageClass manageClassWindow = new();
                ManageClassViewModel manageClassViewModel = new((c) =>
                {
                    // Trigger OnPropertyChanged
                    Classes.RemoveAt(selectedClassIndex);
                    Classes.Insert(selectedClassIndex, c);
                }, () => manageClassWindow.Close(), SelectedClass);
                manageClassWindow.DataContext = manageClassViewModel;
                manageClassWindow.ShowDialog(Essentials.GetMainWindow());
            }, () => SelectedClass != null);
            RemoveClassCommand = new RelayCommand(() =>
            {
                Classes.Remove(SelectedClass!);
            }, () => SelectedClass != null);
        }
    }
}