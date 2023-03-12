using System.Linq;
using Clicker.Game;
using UnityEngine;

namespace Clicker.Core
{
    public class ScreenSystem : MonoBehaviour
    {
        private Transform _transform;
        private GameManagement _game;

        public void Init(Transform transform, GameManagement game)
        {
            _transform = transform;
            _game = game;
        }

        public void ShowGame()
        {
            var screenPrefab = _game.ConfigData.ScreenConfig.Screens.FirstOrDefault(screen => screen.ScreenType == ScreenType.Game);

            if (screenPrefab == null)
                return;

            var screen = Instantiate(screenPrefab, _transform);

            screen.Init(_game);
        }
    }
}
