using Clicker.Core.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Core.Services
{
    public sealed class GameState : IEnterState
    {
        private readonly IGameManagement _game;
        private readonly IStateMachine _stateMachine;
        private readonly IScreenSystem _screenSystem;
        private readonly ICanvasConfig _canvasConfig;

        public GameState(IGameManagement game, IStateMachine stateMachine, IScreenSystem screenSystem, ICanvasConfig canvasConfig)
        {
            _game = game;
            _stateMachine = stateMachine;
            _screenSystem = screenSystem;
            _canvasConfig = canvasConfig;
        }

        public void Enter()
        {
            var gameSceneInfo = new SceneInfo(SceneKeys.GameSceneName, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(gameSceneInfo);
        }

        public void Exit() { }

        private void OnSceneLoad()
        {
            InitScreenSystem();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitScreenSystem()
        {
            var transform = CreateCanvas();

            _screenSystem.Init(_game, transform);
        }

        private Transform CreateCanvas()
        {
            var canvasObject = new GameObject();
            canvasObject.name = _canvasConfig.Name;

            AddCanvas(canvasObject);
            AddCanvasScaler(canvasObject);
            AddGraphicRaycaster(canvasObject);

            return canvasObject.transform;
        }

        private void AddCanvas(GameObject canvasObject)
        {
            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = _canvasConfig.RenderMode;
            canvas.worldCamera = Camera.main;
        }

        private void AddCanvasScaler(GameObject canvasObject)
        {
            var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = _canvasConfig.ScaleMode;
            canvasScaler.referenceResolution = _canvasConfig.ReferenceResolution;
            canvasScaler.matchWidthOrHeight = _canvasConfig.MatchWidthOrHeight;
            canvasScaler.referencePixelsPerUnit = _canvasConfig.ReferencePixelsPerUnit;
        }

        private void AddGraphicRaycaster(GameObject canvasObject)
        {
            canvasObject.AddComponent<GraphicRaycaster>();
        }
    }
}
