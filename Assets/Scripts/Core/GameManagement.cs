using System;
using Clicker.Game.Systems;
using Leopotam.Ecs;
using UniRx;

namespace Clicker.Core
{
    public class GameManagement
    {
        private readonly ConfigData _configData;
        private readonly ScreenSystem _screenSystem;

        private readonly Subject<Null> _viewUpdated;

        private readonly StateMachine _stateMachine;

        private GameWorld _world;

        private EcsSystems _gameSystems;

        public ConfigData ConfigData => _configData;

        public ScreenSystem ScreenSystem => _screenSystem;

        public StateMachine StateMachine => _stateMachine;

        public GameWorld World => _world;

        public IObservable<Null> ViewUpdated => _viewUpdated;

        public GameManagement(ConfigData configData, ScreenSystem screenSystem)
        {
            _configData = configData;
            _screenSystem = screenSystem;

            _viewUpdated = new Subject<Null>();

            var sceneLoader = new SceneLoader();
            _stateMachine = new StateMachine(this, sceneLoader);

            _world = new GameWorld(screenSystem);

            _gameSystems = CreateGameSystems();

            _gameSystems.Init();
        }

        public void Tick(float deltaTime)
        {
            _gameSystems.Run();
        }

        public void ViewUpdate()
        {
            _viewUpdated.OnNext(null);
        }

        public void LateTick()
        {
        }

        public void Destroy()
        {
            _gameSystems?.Destroy();
            _gameSystems = null;

            _world?.Destroy();
            _world = null;
        }

        private EcsSystems CreateGameSystems()
        {
            var systems = new EcsSystems(_world)
                .Add(new LevelUpSystem(_world))
                .Add(new ImprovementPurchaseSystem(_world))
                .Add(new IncomeSystem(_world));

            return systems;
        }
    }
}
