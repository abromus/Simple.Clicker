using UnityEngine;

namespace Clicker.Core
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "Settings/ConfigData")]
    public class ConfigData : ScriptableObject
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        [SerializeField] private BusinessConfig _businessConfig;
        [SerializeField] private CanvasConfig _canvasConfig;
        [SerializeField] private ScreenConfig _screenConfig;
        [SerializeField] private LocalizationConfig _localizationConfig;

        public ApplicationConfig ApplicationConfig => _applicationConfig;

        public BusinessConfig BusinessConfig => _businessConfig;

        public CanvasConfig CanvasConfig => _canvasConfig;

        public ScreenConfig ScreenConfig => _screenConfig;

        public LocalizationConfig LocalizationConfig => _localizationConfig;
    }
}
