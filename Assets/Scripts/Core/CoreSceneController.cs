using UnityEngine;

namespace Clicker.Core
{
    public class CoreSceneController : MonoBehaviour
    {
        [SerializeField] private ConfigData _configData;
        [SerializeField] private ScreenSystem _screenSystem;

        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            _game?.Destroy();
        }

        public void CreateGame()
        {
            _game = new Game(_configData, _screenSystem);

            EnterInitState();
        }

        private void EnterInitState()
        {
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}
