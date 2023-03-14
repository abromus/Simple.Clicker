using Clicker.Core.Settings;

namespace Clicker.Core.Services
{
    public sealed class ServiceOptions : IServiceOptions
    {
        public IGameManagement GameManagement { get; }

        public IScreenSystem ScreenSystem { get; }

        public ILocalizationConfig LocalizationConfig { get; }

        public IUiFactoryConfig UiFactoryConfig { get; }

        public IUiServiceConfig UiServiceConfig { get; }

        public ServiceOptions(
            IGameManagement gameManagement,
            ILocalizationConfig localizationConfig,
            IUiFactoryConfig uiFactoryConfig,
            IUiServiceConfig uiServiceConfig)
        {
            GameManagement = gameManagement;
            LocalizationConfig = localizationConfig;
            UiFactoryConfig = uiFactoryConfig;
            UiServiceConfig = uiServiceConfig;
        }
    }
}
