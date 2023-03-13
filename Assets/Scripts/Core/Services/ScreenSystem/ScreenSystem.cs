using System.Linq;
using Clicker.Core.Factories;
using Clicker.Game.Screens;
using UnityEngine;

namespace Clicker.Core.Services
{
    public sealed class ScreenSystem : UiService, IScreenSystem
    {
        private IGameManagement _game;
        private Transform _transform;

        public override UiServiceType UiServiceType => UiServiceType.ScreenSystem;

        public void Init(IGameManagement game, Transform transform)
        {
            _game = game;
            _transform = transform;
        }

        public void ShowGame()
        {
            var screenPrefab = _game.ConfigData.ScreenConfig.Screens
                .FirstOrDefault(screen => screen.ScreenType == ScreenType.Game);

            if (screenPrefab == null)
                return;

            var screen = Instantiate(screenPrefab, _transform);

            var options = new GameScreenOptions(
                _game.World,
                _game.ConfigData,
                _game.ServiceStorage.GetService<ILocalizationSystem>(),
                _game.ViewUpdated,
                _game.ServiceStorage.GetService<IFactoryStorage>().UiFactories);

            screen.Init(options);
        }
    }
}
