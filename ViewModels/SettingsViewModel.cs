using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;
using WielkaSowa.Helpers;
using WielkaSowa.Models;
using WielkaSowa.Services;
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
        public AsyncRelayCommand CreateMultipliersFileCommand { get; }
        public RelayCommand DefaultMultipliersCommand { get; }
        public RelayCommand EditMultipliersFileCommand { get; }

        private SettingsModel _temp;
        public SettingsModel Temp 
        {
            get => _temp;
            private set => SetProperty(ref _temp, value); 
        }

        public SettingsViewModel()
        {
            _temp = SettingsModel.Clone(Settings.Instance.Current);
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
                var res = await ofd.ShowAsync(Essentials.GetWindowOfType(typeof(SettingsWindow))!);
                if (res == null || res.Length == 0) return;
                string file = res[0];
                Temp.PathToCustomMultipliers = file;
            });
            DefaultMultipliersCommand = new RelayCommand(() =>
            {
                Temp.PathToCustomMultipliers = Multipliers.DefaultWildcard;
            });
            CreateMultipliersFileCommand = new AsyncRelayCommand(async () =>
            {
                var sfd = new SaveFileDialog
                {
                    Title = "Utwórz nowy plik ze współcztnnikami",
                    Filters = Constants.DataFileFilters,
                    InitialFileName = "Multipliers"
                };
                var res = await sfd.ShowAsync(Essentials.GetWindowOfType(typeof(SettingsWindow))!);
                if (string.IsNullOrEmpty(res)) return;
                string json = JsonConvert.SerializeObject(Multipliers.Default);
                await new FileManager(res).Write(json);
            });
            EditMultipliersFileCommand = new RelayCommand(() =>
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer",
                    Arguments = $"\"{Temp.PathToCustomMultipliers}\""
                });
            }, () => Temp.PathToCustomMultipliers != Multipliers.DefaultWildcard);
        }

        private static void Close()
        {
            Essentials.CloseTopWindow();
        }
    }
}