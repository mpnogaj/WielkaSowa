using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using WielkaSowa.Views;

namespace WielkaSowa.Helpers
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

        public static Window? GetWindowOfType(Type windowType)
        {
            if (GetAppDesktopLifetime().Windows.Count < 2)
            {
                return null;
            }

            foreach (var window in GetAppDesktopLifetime().Windows)
            {
                if (window.GetType() == windowType) return window;
            }

            return null;
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