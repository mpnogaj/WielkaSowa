using System.ComponentModel;
using Newtonsoft.Json;

namespace WielkaSowa.Helpers
{
	public class Settings
	{
		const string SettingsFile = "settings.json";
        public static Settings? Instance { get; private set; } = null;

        private SettingsModel _current = new();
        public SettingsModel Current
        {
            get => _current;
            private set
            {
                if(value != _current)
                {
                    _current = value;
                }
            }
        }

		public SettingsModel Default { get; private set; } = new()
        {
            SimpleUI = false
        };

		public void RevertToDefault()
        {
			Current = SettingsModel.Clone(Default);
        }

		public void ApplySettings(SettingsModel settings)
        {
			Current = SettingsModel.Clone(settings);
            Current.ApplySettings();
        }

		public async Task LoadSettings()
        {
			string json;
            try
            {
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
                Current.ApplySettings();
            }
        }

        public async Task SaveSettings()
        {
			string json = JsonConvert.SerializeObject(Current);
            using StreamWriter sw = new(SettingsFile);
            await sw.WriteAsync(json);
        }

        private Settings()
        {
            
        }

        public static void Init()
        {
            Instance = new Settings();
        }
	}
}