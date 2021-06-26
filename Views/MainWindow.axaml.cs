using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var viewmodel = new MainWindowViewModel();
            this.DataContext = viewmodel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}