using System.Collections.Generic;
using UnityEngine;
using Screen = Clicker.Game.Screen;

namespace Clicker.Core
{
    [CreateAssetMenu(fileName = "ScreenConfig", menuName = "Settings/ScreenConfig")]
    public class ScreenConfig : ScriptableObject
    {
        [SerializeField] private List<Screen> _screens;

        public IReadOnlyList<Screen> Screens => _screens;
    }
}
