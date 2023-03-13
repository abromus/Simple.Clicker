using System;
using System.Collections.Generic;
using Clicker.Core.Services;
using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "UiServiceConfig", menuName = "Settings/UiServiceConfig")]
    public sealed class UiServiceConfig : ScriptableObject
    {
        [SerializeField] private List<UiService> _uiService;

        public IReadOnlyList<UiService> UiServices => _uiService;
    }
}
