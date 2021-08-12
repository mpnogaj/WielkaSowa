using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var viewmodel = new SettingsViewModel();
            this.DataContext = viewmodel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}