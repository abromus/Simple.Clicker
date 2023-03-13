using Clicker.Core.Components;
using Clicker.Core.Services;
using Clicker.Core.Settings;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Core
{
    public sealed class CoreSceneController : MonoBehaviour
    {
        [SerializeField] private ConfigData _configData;

        private IGameManagement _game;

        private void Awake()
        {
            Application.targetFrameRate = _configData.ApplicationConfig.TargetFrameRate;

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game?.Tick(Time.deltaTime);

            _game.ViewUpdate();

            _game?.LateTick();
        }

        private void OnApplicationPause(bool pause)
        {
            SaveGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private void OnDestroy()
        {
            _game?.Destroy();
        }

        public void CreateGame()
        {
            _game = new GameManagement(_configData);

            EnterInitState();
        }

        private void EnterInitState()
        {
            _game.ServiceStorage.GetService<IStateMachine>().Enter<BootstrapState>();
        }

        private void SaveGame()
        {
            var saveEntity = _game.World.NewEntity();
            ref var save = ref saveEntity.Get<Save>();

            _game?.LateTick();
        }
    }
}
