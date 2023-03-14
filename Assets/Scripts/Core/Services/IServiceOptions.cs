using Clicker.Core.Settings;

namespace Clicker.Core.Services
{
    public interface IServiceOptions
    {
        public IGameManagement GameManagement { get; }

        public IScreenSystem ScreenSystem { get; }

        public ILocalizationConfig LocalizationConfig { get; }

        public IUiFactoryConfig UiFactoryConfig { get; }

        public IUiServiceConfig UiServiceConfig { get; }
    }
}
