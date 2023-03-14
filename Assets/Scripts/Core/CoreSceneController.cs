using Clicker.Core.Components;
using Clicker.Core.Services;
using Clicker.Core.Settings;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Core
{
    public sealed class CoreSceneController : MonoBehaviour
    {
        [SerializeField] private ConfigStorage _configStorage;

        private IGameManagement _game;

        private void Awake()
        {
            _configStorage.Init();
            Application.targetFrameRate = _configStorage.GetUiConfig<IApplicationConfig>().TargetFrameRate;

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (_game == null)
                return;

            _game.Tick(Time.deltaTime);

            _game.ViewUpdate();

            _game.LateTick();
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
            _game = new GameManagement(_configStorage);

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

            _game.LateTick();
        }
    }
}
