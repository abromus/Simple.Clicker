using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Clicker.Core.Saves
{
    public class SaveManager
    {
        private readonly SavePathProvider _pathProvider;
        private readonly string _backupPath;
        private readonly string _filePath;

        public SaveManager()
        {
            var pathProviderFactory = new SavePathProviderFactory();
            _pathProvider = pathProviderFactory.Create();

            _backupPath = $"{_pathProvider.FileDirectory}/{_pathProvider.BackupFileName}";
            _filePath = $"{_pathProvider.FileDirectory}/{_pathProvider.FileName}";
        }

        public void Save(string save)
        {
            try
            {
                if (!Directory.Exists(_pathProvider.FileDirectory))
                    Directory.CreateDirectory(_pathProvider.FileDirectory);

                using (var writer = File.Create(_backupPath))
                {
                    writer.Write(Encoding.UTF8.GetBytes(save), 0, save.Length);
                    writer.Flush();
                }

                if (File.Exists(_filePath))
                {
                    File.Replace(_backupPath, _filePath, null);
                }
                else
                {
                    File.Move(_backupPath, _filePath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}");
            }
        }

        public string Load()
        {
            if (!File.Exists(_filePath))
                return null;

            try
            {
                byte[] bytes;

                using (var reader = File.OpenRead(_filePath))
                {
                    bytes = new byte[reader.Length];
                    reader.Read(bytes, 0, bytes.Length);
                }

                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}");
            }

            return null;
        }
    }
}
