using UnityEngine;

namespace Clicker.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private EcsStartup _ecsStartup;

        private Game _game;

        private void Awake()
        {
            CreateGame();

            EnterInitState();

            DontDestroyOnLoad(this);
        }

        private void CreateGame()
        {
            _game = new Game(_ecsStartup);
        }

        private void EnterInitState()
        {
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}
