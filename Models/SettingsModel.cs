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
		
		public SettingsModel()
		{
			_simpleUI = false;
		}
	}
}