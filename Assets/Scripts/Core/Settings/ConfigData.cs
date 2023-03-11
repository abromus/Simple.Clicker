﻿using UnityEngine;

namespace Clicker.Core
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "Settings/ConfigData")]
    public class ConfigData : ScriptableObject
    {
        [SerializeField] private CanvasConfig _canvasConfig;
        [SerializeField] private ScreenConfig _screenConfig;
        [SerializeField] private BusinessConfig _businessConfig;

        public CanvasConfig CanvasConfig => _canvasConfig;

        public ScreenConfig ScreenConfig => _screenConfig;

        public BusinessConfig BusinessConfig => _businessConfig;
    }
}