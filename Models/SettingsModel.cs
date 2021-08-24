using Avalonia;
using System;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Helpers
{
	public class SettingsModel : ViewModelBase
	{
		private bool _simpleUI = false;
		public bool SimpleUI 
		{ 
			get => _simpleUI;
			set => SetProperty(ref _simpleUI, value);
		}
		public SettingsModel()
		{
			_simpleUI = false;
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
				SimpleUI = settingsModel.SimpleUI
			};
        }
    }
}