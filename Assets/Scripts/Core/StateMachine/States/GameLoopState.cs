namespace Clicker.Core
{
    public class GameLoopState : IEnterState
    {
        private readonly Game _game;

        public GameLoopState(Game game)
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
