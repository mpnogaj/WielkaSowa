using System.Threading.Tasks;
using Avalonia.Controls;
using WielkaSowa.Helpers;
using WielkaSowa.Models;
using WielkaSowa.ViewModels.Commands;
using WielkaSowa.ViewModels.Commands.Async;
using WielkaSowa.Views;

namespace WielkaSowa.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand RevertToDefaultCommand { get; }
        public AsyncRelayCommand ChooseMultipliersFileCommand { get; }
        public RelayCommand DefaultMultipliersCommand { get; }

        private SettingsModel _temp;
        public SettingsModel Temp 
        {
            get => _temp;
            private set => SetProperty(ref _temp, value); 
        }

        public SettingsViewModel()
        {
            _temp = SettingsModel.Clone(Settings.Instance!.Current);
            CancelCommand = new RelayCommand(Close);
            SaveCommand = new RelayCommand(() =>
            {
                Task.Run(() => Settings.Instance.ApplySettings(Temp)).Wait();
                Close();
            });
            RevertToDefaultCommand = new RelayCommand(() =>
            {
                Settings.Instance.RevertToDefault();
                Temp = SettingsModel.Clone(Settings.Instance.Default);
            });
            ChooseMultipliersFileCommand = new AsyncRelayCommand(async () =>
            {
                var ofd = new OpenFileDialog
                {
                    AllowMultiple = false,
                    Filters = Constants.DataFileFilters,
                    Title = "Wybierz plik ze współczynnikami"
                };
                string file = (await ofd.ShowAsync(Essentials.GetWindowOfType(typeof(SettingsWindow))))[0];
                Temp.PathToCustomMultipliers = file;
            });
            DefaultMultipliersCommand = new RelayCommand(() =>
            {
                Temp.PathToCustomMultipliers = Multipliers.DefaultWildcard;
            });
        }

        private static void Close()
        {
            Essentials.CloseTopWindow();
        }
    }
}