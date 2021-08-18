using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WielkaSowa.Helpers.Calculators;
using WielkaSowa.Models;

namespace WielkaSowa.Helpers
{
    public class Storage
    {
        public string CurrentFile { get; private set; }
        private FileStream? _file;
        public static Storage? Instance = null;

        public event EventHandler ClassesUpdated;

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

        private Storage(string defaultFile)
        {
            _classes = new List<Class>();
            CurrentFile = defaultFile;
        }

        private void OpenFile(string? filePath)
        {
            CloseFile();
            if (filePath != null)
            {
                CurrentFile = filePath;
            }
            if (CurrentFile == string.Empty || !File.Exists(CurrentFile)) return;
            _file = new FileStream(CurrentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 1024, true);
            _file.Lock(0, _file.Length);
        }

        private async Task LoadFileContent()
        {
            if (_file == null) return;
            Debug.WriteLine("Czytam...");
            try
            {
                string json = await ReadFromFile(_file);
                var jsonObj = JsonConvert.DeserializeObject<List<Class>>(json);
                Debug.WriteLine("Koniec...");
                Classes = jsonObj ?? new List<Class>();
            }
            catch (JsonException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task WriteToFile(string content, FileStream file)
        {

            byte[] bytes = Encoding.UTF8.GetBytes(content);
            file.Seek(0, SeekOrigin.Begin);
            await file.WriteAsync(bytes, 0, bytes.Length);
        }

        private async Task<string> ReadFromFile(FileStream file)
        {
            byte[] bytes = new byte[file.Length];
            await file.ReadAsync(bytes, 0, (int)file.Length);
            return Encoding.UTF8.GetString(bytes);
        }

        public void CloseFile()
        {
            _file?.Unlock(0, _file.Length);
            _file?.Close();
        }

        public async Task OpenAndLoadFile(string? filePath = null)
        {
            OpenFile(filePath);
            await LoadFileContent();
        }

        public async Task SaveToFile(string? filePath = null)
        {
            string json = JsonConvert.SerializeObject(Classes);
            if (_file != null)
            {
                await WriteToFile(json, _file);
            }
            else if (filePath != null)
            {
                var file = File.Create(filePath);
                await WriteToFile(json, file);
                file.Close();
                OpenFile(filePath);
            }
        }

        public void UpdateCalcs()
        {
            AttendenceCalc.UpdatePoints();
            MarkCalculator.UpdatePoints();
        }

        public static void Init(string file = "")
        {
            Instance = new Storage(file);
        }
    }
}
