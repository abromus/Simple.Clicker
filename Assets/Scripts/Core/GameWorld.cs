using Leopotam.Ecs;

namespace Clicker.Core
{
    public class GameWorld : EcsWorld
    {
        private readonly ScreenSystem _screenSystem;
        private readonly State _state;

        public ScreenSystem ScreenSystem => _screenSystem;

        public State State => _state;

        public GameWorld(ScreenSystem screenSystem) : base()
        {
            _screenSystem = screenSystem;
            _state = new State();
        }
    }
}
