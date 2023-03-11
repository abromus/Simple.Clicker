using Leopotam.Ecs;

namespace Clicker.Core
{
    public class Game
    {
        private readonly ConfigData _configData;
        private readonly ScreenSystem _screenSystem;

        private readonly StateMachine _stateMachine;

        private GameWorld _world;

        public ConfigData ConfigData => _configData;

        public ScreenSystem ScreenSystem => _screenSystem;

        public StateMachine StateMachine => _stateMachine;

        public GameWorld World => _world;

        public Game(ConfigData configData, ScreenSystem screenSystem)
        {
            _configData = configData;
            _screenSystem = screenSystem;

            var sceneLoader = new SceneLoader();
            _stateMachine = new StateMachine(this, sceneLoader);

            _world = new GameWorld(screenSystem);
        }

        public void Destroy()
        {
            _world?.Destroy();
            _world = null;
        }
    }
}
