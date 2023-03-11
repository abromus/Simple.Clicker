using System.Linq;
using Clicker.Game;
using UnityEngine;

namespace Clicker.Core
{
    public class ScreenSystem : MonoBehaviour
    {
        private Transform _transform;
        private ScreenConfig _screenConfig;

        public void Init(Transform transform, ScreenConfig screenConfig)
        {
            _transform = transform;
            _screenConfig = screenConfig;
        }

        public void ShowGame()
        {
            var screen = _screenConfig.Screens.FirstOrDefault(screen => screen.ScreenType == ScreenType.Game);

            if (screen == null)
                return;

            Instantiate(screen, _transform);
        }
    }
}
