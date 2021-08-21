using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}