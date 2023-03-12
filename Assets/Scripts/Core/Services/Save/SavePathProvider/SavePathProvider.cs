namespace Clicker.Core.Saves
{
    public abstract class SavePathProvider
    {
        public abstract string FileDirectory { get; }
        public abstract string FileName { get; }
        public abstract string BackupFileName { get; }
    }
}
