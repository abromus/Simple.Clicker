namespace Clicker.Core
{
    public class GameState : IEnterState
    {
        private const string GameSceneName = "GameScene";

        private readonly StateMachine _stateMachine;

        public GameState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var gameSceneInfo = new SceneInfo(GameSceneName, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(gameSceneInfo);
        }

        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
