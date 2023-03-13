using Clicker.Core.Services;

namespace Clicker.Core.Factories
{
    public sealed class SavePathProviderFactory : ISavePathProviderFactory
    {
        public SavePathProvider Create()
        {
#if UNITY_EDITOR
            return new EditorSavePathProvider();
#else
            return new AndroidSavePathProvider();
#endif
        }
    }
}
