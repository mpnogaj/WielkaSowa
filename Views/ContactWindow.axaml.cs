using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Views
{
    public partial class ContactWindow : Window
    {
        public ContactWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var viewmodel = new ContactViewModel();
            this.DataContext = viewmodel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
