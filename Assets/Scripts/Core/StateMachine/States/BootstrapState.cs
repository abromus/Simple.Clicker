namespace Clicker.Core
{
    public class BootstrapState : IEnterState
    {
        private const string CoreSceneName = "CoreScene";

        private readonly StateMachine _stateMachine;

        public BootstrapState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var coreSceneInfo = new SceneInfo(CoreSceneName, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(coreSceneInfo);
        }

        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameState>();
        }
    }
}
