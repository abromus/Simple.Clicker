using UnityEngine;

namespace Clicker.Core.Saves
{
    internal class AndroidSavePathProvider : SavePathProvider
    {
        private const string AndroidFileName = "save";
        private const string AndroidBackupFileName = "save.backup";

        private string _androidDataPath;

        public override string FileDirectory => _androidDataPath ??= GetAndroidDataPath();

        public override string FileName => AndroidFileName;

        public override string BackupFileName => AndroidBackupFileName;

        private static string GetAndroidDataPath()
        {
#if UNITY_ANDROID
            using var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            using var currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            using var filesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir");

            var path = filesDir.Call<string>("getAbsolutePath");
            
            return path;
#else
            return null;
#endif
        }
    }
}
