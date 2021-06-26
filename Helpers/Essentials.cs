using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using WielkaSowa.Views;

namespace WielkaSowa
{
    public static class Essentials
    {
        public static IClassicDesktopStyleApplicationLifetime GetAppDesktopLifetime()
        {
            return (IClassicDesktopStyleApplicationLifetime) Application.Current.ApplicationLifetime;
        }

        public static MainWindow GetMainWindow()
        {
            return (GetAppDesktopLifetime().MainWindow as MainWindow)!;
        }

        public static void CloseTopWindow()
        {
            Window w = GetAppDesktopLifetime().Windows[^1];
            if(w is not MainWindow) w.Close();
        }
    }
}