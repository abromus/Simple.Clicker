using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Settings/LocalizationConfig")]
    public sealed class LocalizationConfig : ScriptableObject
    {
        [SerializeField] private List<LocalizationData> _localizationData;

        public IReadOnlyList<LocalizationData> LocalizationData => _localizationData;
    }
}
