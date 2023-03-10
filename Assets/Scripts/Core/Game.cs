using Leopotam.Ecs;

namespace Clicker.Core
{
    public class Game
    {
        private readonly EcsStartup _ecsStartup;

        private readonly StateMachine _stateMachine;
        private readonly EcsWorld _world;

        public StateMachine StateMachine => _stateMachine;

        public Game(EcsStartup ecsStartup)
        {
            _ecsStartup = ecsStartup;
            _stateMachine = new StateMachine(new SceneLoader());

            _world = ecsStartup.World;
        }
    }
}
