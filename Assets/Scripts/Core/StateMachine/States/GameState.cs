using UnityEngine.UI;
using UnityEngine;

namespace Clicker.Core
{
    public class GameState : IEnterState
    {
        private const string GameSceneName = "GameScene";

        private readonly Game _game;

        public GameState(Game game)
        {
            _game = game;
        }

        public void Enter()
        {
            var gameSceneInfo = new SceneInfo(GameSceneName, OnSceneLoad);

            _game.StateMachine.Enter<SceneLoaderState, SceneInfo>(gameSceneInfo);
        }

        public void Exit() { }

        private void OnSceneLoad()
        {
            InitScreenSystem();

            _game.StateMachine.Enter<GameLoopState>();
        }
        
        private void InitScreenSystem()
        {
            var transform = CreateCanvas();

            _game.ScreenSystem.Init(transform, _game.ConfigData.ScreenConfig);
        }

        private Transform CreateCanvas()
        {
            var config = _game.ConfigData.CanvasConfig;

            var canvasObject = new GameObject();
            canvasObject.name = config.Name;

            AddCanvas(config, canvasObject);
            AddCanvasScaler(config, canvasObject);
            AddGraphicRaycaster(canvasObject);

            return canvasObject.transform;
        }

        private static void AddCanvas(CanvasConfig config, GameObject canvasObject)
        {
            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = config.RenderMode;
            canvas.worldCamera = Camera.main;
        }

        private static void AddCanvasScaler(CanvasConfig config, GameObject canvasObject)
        {
            var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = config.ScaleMode;
            canvasScaler.referenceResolution = config.ReferenceResolution;
            canvasScaler.matchWidthOrHeight = config.MatchWidthOrHeight;
            canvasScaler.referencePixelsPerUnit = config.ReferencePixelsPerUnit;
        }

        private static void AddGraphicRaycaster(GameObject canvasObject)
        {
            canvasObject.AddComponent<GraphicRaycaster>();
        }
    }
}
