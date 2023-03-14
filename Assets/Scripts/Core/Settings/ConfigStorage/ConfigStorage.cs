using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "ConfigStorage", menuName = "Settings/ConfigStorage")]
    public sealed class ConfigStorage : ScriptableObject, IConfigStorage
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        [SerializeField] private BusinessConfig _businessConfig;
        [SerializeField] private CanvasConfig _canvasConfig;
        [SerializeField] private LocalizationConfig _localizationConfig;
        [SerializeField] private ScreenConfig _screenConfig;
        [SerializeField] private UiFactoryConfig _uiFactoryConfig;
        [SerializeField] private UiServiceConfig _uiServiceConfig;

        private Dictionary<Type, IUiConfig> _configs;

        public void Init()
        {
            _configs = new Dictionary<Type, IUiConfig>()
            {
                [typeof(IApplicationConfig)] = _applicationConfig,
                [typeof(IBusinessConfig)] = _businessConfig,
                [typeof(ICanvasConfig)] = _canvasConfig,
                [typeof(ILocalizationConfig)] = _localizationConfig,
                [typeof(IScreenConfig)] = _screenConfig,
                [typeof(IUiFactoryConfig)] = _uiFactoryConfig,
                [typeof(IUiServiceConfig)] = _uiServiceConfig,
            };
        }

        public TUiConfig GetUiConfig<TUiConfig>() where TUiConfig : class, IUiConfig
        {
            return _configs[typeof(TUiConfig)] as TUiConfig;
        }
    }
}
