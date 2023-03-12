using UnityEngine;

namespace Clicker.Core.Saves
{
    public class DefaultSavePathProvider : SavePathProvider
    {
        private const string DefaultFileName = "save";
        private const string DefaultBackupFileName = "save.backup";

        private readonly string _directoryPath = Application.persistentDataPath;

        public override string FileDirectory => _directoryPath;

        public override string FileName => DefaultFileName;

        public override string BackupFileName => DefaultBackupFileName;
    }
}
