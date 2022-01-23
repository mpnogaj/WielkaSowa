using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using WielkaSowa.Helpers;
using WielkaSowa.Services;
using WielkaSowa.Views;

namespace WielkaSowa
{
    public class App : Application
    {
        private const string LightThemeKey = "ThemeLight", DarkThemeKey = "ThemeDark";
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Logger.Init();
            Storage.Init();
            Settings.Init();
            Styles.Insert(0, (FluentTheme)Resources[LightThemeKey]!);
            Task.Run(() => Settings.Instance!.LoadSettings()).Wait();
            if (Settings.Instance!.Current.DarkTheme)
            {
                Current.Resources["WindowBackground"] = new SolidColorBrush(Color.Parse("#EE202120"));
                ChangeTheme(FluentThemeMode.Dark);
            }
            else
            {
                Current.Resources["WindowBackground"] = new SolidColorBrush(Color.Parse("#FFFFFFFF"));
                ChangeTheme(FluentThemeMode.Light);
            }
        }

        private static void OnStartup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            Task.Run(() => Storage.Instance!.OpenAndLoadFile(args.Length > 1 && File.Exists(args[1]) ? args[1] : null)).Wait();
        }

        private static void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            Task.Run(() => Settings.Instance!.SaveSettings()).Wait();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.Startup += OnStartup;
                desktop.Exit += OnExit;
            }
            
            base.OnFrameworkInitializationCompleted();
        }

        private static void ChangeTheme(FluentThemeMode theme)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                Current.Styles[0] = theme switch
                {
                    FluentThemeMode.Dark => (FluentTheme)Current.Resources[DarkThemeKey]!,
                    _ => (FluentTheme)Current.Resources[LightThemeKey]!,
                };
            });
        }
    }
}