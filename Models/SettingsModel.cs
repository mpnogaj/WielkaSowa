using System.Threading.Tasks;
using Avalonia;
using Newtonsoft.Json;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Models
{
	public class SettingsModel : ViewModelBase
	{
		private bool _simpleUi;
		public bool SimpleUi 
		{ 
			get => _simpleUi;
			set => SetProperty(ref _simpleUi, value);
		}

		private bool _darkTheme;
		public bool DarkTheme
        {
			get => _darkTheme;
			set => SetProperty(ref _darkTheme, value);
        }

		private string _pathToCustomMultipliers;
		public string PathToCustomMultipliers
		{
			get => _pathToCustomMultipliers;
			set  => SetProperty(ref _pathToCustomMultipliers, value);
		}
		
		[JsonIgnore]
		public Multipliers Multipliers { get; private set; }

		public SettingsModel()
		{
			_simpleUi = false;
			_darkTheme = true;
			_pathToCustomMultipliers = "";
			Multipliers = Multipliers.Default;
		}

		public async Task ApplySettings()
        {
			var res = Application.Current.Resources;
			res["ComplexUI"] = !SimpleUi;
			res["TransparencyHint"] = Converters.BoolToWindowTransparencyLevel.ToWindowTransparencyLevel(SimpleUi);
			Multipliers = await Multipliers.CreateMultipliers(_pathToCustomMultipliers);
        }

		public static SettingsModel Clone(SettingsModel settingsModel)
        {
			return new SettingsModel()
			{
				SimpleUi = settingsModel.SimpleUi,
				DarkTheme = settingsModel.DarkTheme,
				PathToCustomMultipliers = settingsModel.PathToCustomMultipliers
			};
        }
    }
}