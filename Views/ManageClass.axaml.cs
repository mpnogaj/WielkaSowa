using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WielkaSowa.Views
{
    public class ManageClass : Window
    {
        public ManageClass()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}