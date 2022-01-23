using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WielkaSowa.Services
{
	public class FileManager
	{
		public string FilePath
		{
			get => _file;
		}

		private readonly string _file;
		private readonly bool _append; 

		public FileManager(string path, bool shouldAppend)
		{
			_file = path;
			_append = shouldAppend;
		}

		#region Methods
		#region Write

		/// <summary>
		/// Writes to file
		/// </summary>
		/// <param name="content">String to write to file</param>
		public async Task Write(string content)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(content);
			using var sw = File.OpenWrite(_file);
			sw.Seek(0, _append ? SeekOrigin.End : SeekOrigin.Begin);
			await sw.WriteAsync(bytes);
			sw.Close();
		}

		public async Task WriteLine(object line)
		{
			await Write(line.ToString() + Environment.NewLine);
		}

		public async Task WriteLines(ICollection<object> lines)
		{
			var sb = new StringBuilder();
			foreach (var line in lines)
			{
				sb.AppendLine(line.ToString());
			}
			await Write(sb.ToString());
		}

		#endregion
		#region Read

		public async Task<string> Read()
		{
			using var sr = File.OpenRead(_file);
			byte[] bytes = new byte[sr.Length];
			await sr.ReadAsync(bytes);
			return Encoding.UTF8.GetString(bytes);
		}

		public async Task<ICollection<string>> ReadLines()
		{
			return (await Read()).Split(Environment.NewLine);
		}

		#endregion
		#region Other
		public void Clear()
		{
			Task.Run(() => Write(string.Empty));
		}
		#endregion
		#endregion
	}
}