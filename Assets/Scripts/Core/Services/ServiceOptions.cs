using Clicker.Core.Settings;

namespace Clicker.Core.Services
{
    public sealed class ServiceOptions
    {
        public IGameManagement GameManagement { get; }

        public IScreenSystem ScreenSystem { get; }

        public LocalizationConfig LocalizationConfig { get; }

        public UiFactoryConfig UiFactoryConfig { get; }

        public UiServiceConfig UiServiceConfig { get; }

        public ServiceOptions(
            GameManagement gameManagement,
            LocalizationConfig localizationConfig,
            UiFactoryConfig uiFactoryConfig,
            UiServiceConfig uiServiceConfig)
        {
            GameManagement = gameManagement;
            LocalizationConfig = localizationConfig;
            UiFactoryConfig = uiFactoryConfig;
            UiServiceConfig = uiServiceConfig;
        }
    }
}
