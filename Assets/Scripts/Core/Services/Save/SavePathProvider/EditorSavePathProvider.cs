namespace Clicker.Core.Services
{
    public sealed class EditorSavePathProvider : SavePathProvider
    {
        private const string EditorFileName = "save";
        private const string EditorBackupFileName = "save.backup";

        private const string FileExtension = "txt";
        private const string DirectoryPath = "GameTemp";

        public override string FileDirectory => DirectoryPath;

        public override string FileName => $"{EditorFileName}.{FileExtension}";

        public override string BackupFileName => $"{EditorBackupFileName}.{FileExtension}";
    }
}
