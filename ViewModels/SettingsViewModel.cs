using WielkaSowa.Helpers;
using WielkaSowa.ViewModels.Commands;

namespace WielkaSowa.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand RevertToDefaultCommand { get; }

        private SettingsModel _temp;
        public SettingsModel Temp 
        {
            get => _temp;
            private set => SetProperty(ref _temp, value); 
        }

        public SettingsViewModel()
        {
            _temp = SettingsModel.Clone(Settings.Instance!.Current);
            CancelCommand = new RelayCommand(() =>
            {
                Close();
            });
            SaveCommand = new RelayCommand(() =>
            {
                Settings.Instance!.ApplySettings(Temp);
                Close();
            });
            RevertToDefaultCommand = new RelayCommand(() =>
            {
                Settings.Instance!.RevertToDefault();
                Temp = SettingsModel.Clone(Settings.Instance.Default);
            });
        }

        private static void Close()
        {
            Essentials.CloseTopWindow();
        }
    }
}