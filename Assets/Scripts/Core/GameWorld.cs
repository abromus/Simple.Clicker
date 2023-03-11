using Leopotam.Ecs;

namespace Clicker.Core
{
    public class GameWorld : EcsWorld
    {
        private readonly ScreenSystem _screenSystem;

        public ScreenSystem ScreenSystem => _screenSystem;

        public GameWorld(ScreenSystem screenSystem) : base()
        {
            _screenSystem = screenSystem;
        }
    }
}
