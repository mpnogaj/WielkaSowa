using WielkaSowa.ViewModels.Commands;

namespace WielkaSowa.ViewModels
{
    public class SettingsViewModel
    {
        RelayCommand CloseWindowCommand { get; }
        public SettingsViewModel()
        {
            CloseWindowCommand = new RelayCommand(() =>
            {
                Essentials.CloseTopWindow();
            });
        }
    }
}