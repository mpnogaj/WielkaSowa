using System;
using System.Threading.Tasks;

namespace WielkaSowa.Services
{
	public class Logger
	{
		#region Singleton stuff
		private static Logger? _instance;
		public static Logger Instance
		{
			get => _instance ?? new Logger(new LoggerOptions());
		}

		public static void Init(LoggerOptions? options = null)
		{
			_instance = new Logger(options);
		}
		#endregion

		private FileManager _logFile;
		private Logger(LoggerOptions? options)
		{
			// If null create default options
			if (options == null) options = new LoggerOptions();
			string date = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
			string fileName = options.FileName == string.Empty
				? $"log_{date}.log"
				: options.FileName;
			_logFile = new FileManager(@$"./Logs/{fileName}", options.ShouldAppend);
			Log("Initialized logger!");
		}

		public void Log(string message)
		{
			string date = DateTime.Now.ToString("[HH:mm:ss]");
			Task.Run(() => _logFile.WriteLine($"{date}: {message}"));
		}

		public void Warning(string message)
		{
			Log($"[Warning] {message}");
		}

		public void Error(string message)
		{
			Log($"[!Error!] {message}");
		}
	}

	public class LoggerOptions
	{ 
		public string FileName { get; set; } = string.Empty;
		public bool ShouldAppend { get; set; } = true;
	}
}
