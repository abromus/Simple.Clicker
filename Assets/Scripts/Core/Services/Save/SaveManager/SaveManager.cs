using System;
using System.IO;
using System.Text;
using Clicker.Core.Factories;
using UnityEngine;

namespace Clicker.Core.Services
{
    public sealed class SaveManager: ISaveManager
    {
        private readonly SavePathProvider _pathProvider;

        private readonly string _backupPath;
        private readonly string _filePath;

        public SaveManager(ISavePathProviderFactory pathProviderFactory)
        {
            _pathProvider = pathProviderFactory.Create();

            _backupPath = $"{_pathProvider.FileDirectory}/{_pathProvider.BackupFileName}";
            _filePath = $"{_pathProvider.FileDirectory}/{_pathProvider.FileName}";
        }

        public void Save(string save)
        {
            try
            {
                CheckDirectory();

                WriteSave(save);

                ReplaceFile();
            }
            catch (Exception exception)
            {
                Debug.LogError($"{exception.Message}");
            }
        }

        public string Load()
        {
            if (!File.Exists(_filePath))
                return null;

            try
            {
                var file = ReadSave();

                return file;
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}");
            }

            return null;
        }

        private void CheckDirectory()
        {
            if (!Directory.Exists(_pathProvider.FileDirectory))
                Directory.CreateDirectory(_pathProvider.FileDirectory);
        }

        private void WriteSave(string save)
        {
            using var writer = File.Create(_backupPath);

            writer.Write(Encoding.UTF8.GetBytes(save), 0, save.Length);
            writer.Flush();
        }

        private void ReplaceFile()
        {
            if (File.Exists(_filePath))
                File.Replace(_backupPath, _filePath, null);
            else
                File.Move(_backupPath, _filePath);
        }

        private string ReadSave()
        {
            byte[] bytes;

            using (var reader = File.OpenRead(_filePath))
            {
                bytes = new byte[reader.Length];
                reader.Read(bytes, 0, bytes.Length);
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
