namespace Clicker.Core.Services
{
    public abstract class SavePathProvider
    {
        public abstract string FileDirectory { get; }

        public abstract string FileName { get; }

        public abstract string BackupFileName { get; }
    }
}
