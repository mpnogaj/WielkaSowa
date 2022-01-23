using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WielkaSowa.Helpers.Calculators;
using WielkaSowa.Models;
using WielkaSowa.Services;

namespace WielkaSowa.Helpers
{
	public class Storage
	{
		private static Storage? _instance;
		public static Storage Instance
		{
			get => _instance ?? new Storage();
		}

		public string CurrentFilePath =>
			this._file == null
				? string.Empty
				: this._file.FilePath;

		private Storage()
		{
			_classes = new List<Class>();
		}

		private FileManager? _file;
		

		public event EventHandler? ClassesUpdated;

		private List<Class> _classes;
		public List<Class> Classes
		{
			get => _classes;
			private set
			{
				_classes = value;
				UpdateCalcs();
				ClassesUpdated?.Invoke(this, EventArgs.Empty);
			}
		}

		private void OpenFile(string? filePath)
		{
			if (filePath == null) return;
			_file = filePath == string.Empty || !File.Exists(filePath) 
				? null
				: new FileManager(filePath, false);
		}

		private async Task LoadFileContent()
		{
			if (_file == null)
			{
				Logger.Instance.Warning("Trying to read classes file but it doesn't exist. Aborting");
				return;
			}
			Logger.Instance.Log("Reading classes file...");
			try
			{
				string json = await _file.Read();
				Logger.Instance.Log("Finished reading classes file");
				var jsonObj = JsonConvert.DeserializeObject<List<Class>>(json);
				Classes = jsonObj ?? new List<Class>();
			}
			catch (JsonException ex)
			{
				Logger.Instance.Error("Erorr while parsing classes!");
				Logger.Instance.Error(ex.ToString());
			}
		}

		public async Task OpenAndLoadFile(string? filePath = null)
		{
			OpenFile(filePath);
			await LoadFileContent();
		}

		public async Task SaveToFile(string? newFilePath = null)
		{
			string json = JsonConvert.SerializeObject(Classes);
			if(newFilePath != null)
			{
				_file = new FileManager(newFilePath, false);
			}
			try
			{
				await _file!.Write(json);
			}
			catch (NullReferenceException)
			{
				Logger.Instance.Error("Trying to save classes to file which doesn't exists!");
			}
		}

		public static void UpdateCalcs()
		{
			AttendenceCalc.UpdatePoints();
			MarkCalculator.UpdatePoints();
		}

		public static void Init()
		{
			_instance = new Storage();
		}
	}
}
