using System;
using Clicker.Core.Saves;
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

        private readonly SaveSystem _saveSystem;
        private readonly StateMachine _stateMachine;
        private readonly LocalizationSystem _localizationSystem;
        private GameWorld _world;

        private EcsSystems _initSystems;
        private EcsSystems _gameSystems;
        private EcsSystems _lateSystems;

        public ConfigData ConfigData => _configData;

        public ScreenSystem ScreenSystem => _screenSystem;
        public SaveSystem SaveSystem => _saveSystem;

        public StateMachine StateMachine => _stateMachine;

        public LocalizationSystem LocalizationSystem => _localizationSystem;

        public GameWorld World => _world;

        public IObservable<Null> ViewUpdated => _viewUpdated;

        public GameManagement(ConfigData configData, ScreenSystem screenSystem)
        {
            _configData = configData;
            _screenSystem = screenSystem;

            _viewUpdated = new Subject<Null>();

            var sceneLoader = new SceneLoader();
            _stateMachine = new StateMachine(this, sceneLoader);

            _localizationSystem = new LocalizationSystem(_configData.LocalizationConfig);

            _saveSystem = new SaveSystem();

            _world = new GameWorld(screenSystem, _saveSystem);

            _initSystems = CreateInitSystems();
            _gameSystems = CreateGameSystems();
            _lateSystems = CreateLateSystems();

            _initSystems.Init();
            _gameSystems.Init();
            _lateSystems.Init();
        }

        public void Tick(float deltaTime)
        {
            _world.DeltaTime = deltaTime;

            _initSystems.Run();
            _gameSystems.Run();
        }

        public void ViewUpdate()
        {
            _viewUpdated.OnNext(null);
        }

        public void LateTick()
        {
            _lateSystems.Run();
        }

        public void Destroy()
        {
            _initSystems?.Destroy();
            _initSystems = null;

            _gameSystems?.Destroy();
            _gameSystems = null;

            _lateSystems?.Destroy();
            _lateSystems = null;

            _world?.Destroy();
            _world = null;
        }

        private EcsSystems CreateInitSystems()
        {
            var systems = new EcsSystems(_world)
                .Add(new LoadGameSystem(_world, _saveSystem));

            return systems;
        }

        private EcsSystems CreateGameSystems()
        {
            var systems = new EcsSystems(_world)
                .Add(new TimerSystem(_world))
                .Add(new LevelUpSystem(_world))
                .Add(new ImprovementPurchaseSystem(_world))
                .Add(new IncomeSystem(_world));

            return systems;
        }

        private EcsSystems CreateLateSystems()
        {
            var systems = new EcsSystems(_world)
                .Add(new SaveGameSystem(_saveSystem));

            return systems;
        }
    }
}
