namespace Clicker.Core
{
    public class GameLoopState : IEnterState
    {
        private readonly GameManagement _game;

        public GameLoopState(GameManagement game)
        {
            _game = game;
        }

        public void Enter()
        {
            _game.World.ScreenSystem.ShowGame();
        }

        public void Exit() { }
    }
}
