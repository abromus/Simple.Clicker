namespace Clicker.Core.Saves
{
    public class SavePathProviderFactory
    {
        public SavePathProvider Create()
        {
#if UNITY_EDITOR
            return new EditorSavePathProvider();
#elif UNITY_ANDROID
            return new AndroidSavePathProvider();
#else
            return new DefaultSavePathProvider();
#endif
        }
    }
}
