using System.Collections.Generic;
using UnityEngine;
using Screen = Clicker.Game.Screens.Screen;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "ScreenConfig", menuName = "Settings/ScreenConfig")]
    public sealed class ScreenConfig : ScriptableObject
    {
        [SerializeField] private List<Screen> _screens;

        public IReadOnlyList<Screen> Screens => _screens;
    }
}
