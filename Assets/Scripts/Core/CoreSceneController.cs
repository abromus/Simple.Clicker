using Clicker.Core.Saves.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Core
{
    public class CoreSceneController : MonoBehaviour
    {
        [SerializeField] private ConfigData _configData;
        [SerializeField] private ScreenSystem _screenSystem;

        private GameManagement _game;

        private void Awake()
        {
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
            var saveEntity = _game.World.NewEntity();
            ref var save = ref saveEntity.Get<Save>();

            _game?.LateTick();
        }

        private void OnDestroy()
        {
            _game?.Destroy();
        }

        public void CreateGame()
        {
            _game = new GameManagement(_configData, _screenSystem);

            EnterInitState();
        }

        private void EnterInitState()
        {
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}
