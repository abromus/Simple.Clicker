using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "Settings/ConfigData")]
    public sealed class ConfigData : ScriptableObject
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        [SerializeField] private BusinessConfig _businessConfig;
        [SerializeField] private CanvasConfig _canvasConfig;
        [SerializeField] private LocalizationConfig _localizationConfig;
        [SerializeField] private ScreenConfig _screenConfig;
        [SerializeField] private UiFactoryConfig _uiFactoryConfig;
        [SerializeField] private UiServiceConfig _uiServiceConfig;

        public ApplicationConfig ApplicationConfig => _applicationConfig;

        public BusinessConfig BusinessConfig => _businessConfig;

        public CanvasConfig CanvasConfig => _canvasConfig;

        public LocalizationConfig LocalizationConfig => _localizationConfig;

        public ScreenConfig ScreenConfig => _screenConfig;

        public UiFactoryConfig UiFactoryConfig => _uiFactoryConfig;

        public UiServiceConfig UiServiceConfig => _uiServiceConfig;
    }
}
