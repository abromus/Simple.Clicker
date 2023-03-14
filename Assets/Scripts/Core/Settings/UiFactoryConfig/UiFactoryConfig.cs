using System.Collections.Generic;
using Clicker.Core.Factories;
using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "UiFactoryConfig", menuName = "Settings/UiFactoryConfig")]
    public sealed class UiFactoryConfig : ScriptableObject, IUiFactoryConfig
    {
        [SerializeField] private List<UiFactory> _uiFactories;

        public IReadOnlyList<IUiFactory> UiFactories => _uiFactories;
    }
}
