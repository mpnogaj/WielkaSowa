using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WielkaSowa.Models;

namespace WielkaSowa.Helpers
{
	public class Settings
	{
		const string SettingsFile = "settings.json";
        public static Settings? Instance { get; private set; }

        public SettingsModel Current { get; private set; } = new();

        public SettingsModel Default { get; } = new()
        {
            SimpleUi = false,
            DarkTheme = true,
            PathToCustomMultipliers = Multipliers.DefaultWildcard
        };

		public void RevertToDefault()
        {
			Current = SettingsModel.Clone(Default);
        }

		public async Task ApplySettings(SettingsModel settings)
        {
			Current = SettingsModel.Clone(settings);
            await Current.ApplySettings();
        }

		public async Task LoadSettings()
        {
	        try
            {
	            string json;
	            using (StreamReader sr = new(SettingsFile))
                {
                    json = await sr.ReadToEndAsync();
                }
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

        public async Task SaveSettings()
        {
			string json = JsonConvert.SerializeObject(Current);
            await using StreamWriter sw = new(SettingsFile);
            await sw.WriteAsync(json);
        }

        private Settings() { }

        public static void Init()
        {
            Instance = new Settings();
        }
	}
}