using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using WielkaSowa.Models;

namespace WielkaSowa.Services
{
	public class Settings
	{
		private const string SettingsFile = "settings.json";
		private readonly FileManager _file;

		#region Singleton stuff

		private static Settings? _instance;
		public static Settings Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Settings();
				}
				return _instance;
			}
		}
		public static void Init()
		{
			_instance = new Settings();
		}

		#endregion

		private Settings()
		{
			_file = new FileManager(SettingsFile, false);
		}

		public SettingsModel Current { get; private set; } = new();

		public SettingsModel Default { get; } = new()
		{
			SimpleUi = false,
			DarkTheme = true,
			PathToCustomMultipliers = Multipliers.DefaultWildcard
		};

		public async Task ApplySettings(SettingsModel settings)
		{
			Current = SettingsModel.Clone(settings);
			await Current.ApplySettings();
		}

		public async Task LoadSettings()
		{
			try
			{
				string json = await _file.Read();
				Current = JsonConvert.DeserializeObject<SettingsModel>(json) ?? new SettingsModel();
			}
			catch
			{
				Current = new SettingsModel();
			}
			finally
			{
				await Current.ApplySettings();
			}
		}

		public void RevertToDefault()
		{
			Current = SettingsModel.Clone(Default);
		}

		public async Task SaveSettings()
		{
			string json = JsonConvert.SerializeObject(Current);
			await _file.Write(json);
		}
	}
}