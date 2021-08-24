using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Threading.Tasks;
using WielkaSowa.Helpers;
using WielkaSowa.Views;

namespace WielkaSowa
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Storage.Init();
            Settings.Init();
            Task.Run(() => Settings.Instance!.LoadSettings()).Wait();
        }

        private void OnStartup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            Task.Run(() => Storage.Instance!.OpenAndLoadFile(args.Length > 1 && File.Exists(args[1]) ? args[1] : null)).Wait();
        }

        private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
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
    }
}