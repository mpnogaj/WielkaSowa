using Avalonia;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using System;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Helpers
{
	public class SettingsModel : ViewModelBase
	{
		private bool _simpleUI;
		public bool SimpleUI 
		{ 
			get => _simpleUI;
			set => SetProperty(ref _simpleUI, value);
		}

		private bool _darkTheme;
		public bool DarkTheme
        {
			get => _darkTheme;
			set => SetProperty(ref _darkTheme, value);
        }

        public SettingsModel()
		{
			_simpleUI = false;
			_darkTheme = true;
		}

		public void ApplySettings()
        {
			var res = Application.Current.Resources;
			res["ComplexUI"] = !SimpleUI;
			res["TransparencyHint"] = Converters.BoolToWindowTransparencyLevel.ToWindowTransparencyLevel(SimpleUI);
		}

		public static SettingsModel Clone(SettingsModel settingsModel)
        {
			return new SettingsModel()
			{
				SimpleUI = settingsModel.SimpleUI,
				DarkTheme = settingsModel.DarkTheme
			};
        }
    }
}