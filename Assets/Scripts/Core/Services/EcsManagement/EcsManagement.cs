using Clicker.Core.World;
using Clicker.Game.Systems;
using Leopotam.Ecs;

namespace Clicker.Core.Services
{
    public sealed class EcsManagement : IEcsManagement
    {
        private IWorld _world;

        private EcsSystems _initSystems;
        private EcsSystems _gameSystems;
        private EcsSystems _lateSystems;

        private readonly ISaveSystem _saveSystem;

        public IWorld World => _world;

        public EcsManagement(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;

            _world = new GameWorld(_saveSystem);

            CreateSystems();
            InitSystems();
        }

        public void Tick(float deltaTime)
        {
            _world.DeltaTime = deltaTime;

            _initSystems.Run();
            _gameSystems.Run();
        }

        public void LateTick()
        {
            _lateSystems.Run();
        }

        public void Destroy()
        {
            DestroySystem(_initSystems);
            DestroySystem(_gameSystems);
            DestroySystem(_lateSystems);

            DestroyWorld();
        }

        private void CreateSystems()
        {
            _initSystems = CreateInitSystems();
            _gameSystems = CreateGameSystems();
            _lateSystems = CreateLateSystems();
        }

        private EcsSystems CreateInitSystems()
        {
            var systems = new EcsSystems(_world as EcsWorld)
                .Add(new LoadGameSystem(_world, _saveSystem));

            return systems;
        }

        private EcsSystems CreateGameSystems()
        {
            var systems = new EcsSystems(_world as EcsWorld)
                .Add(new TimerSystem(_world))
                .Add(new LevelUpSystem(_world))
                .Add(new ImprovementPurchaseSystem(_world))
                .Add(new IncomeSystem(_world));

            return systems;
        }

        private EcsSystems CreateLateSystems()
        {
            var systems = new EcsSystems(_world as EcsWorld)
                .Add(new SaveGameSystem(_saveSystem));

            return systems;
        }

        private void InitSystems()
        {
            _initSystems.Init();
            _gameSystems.Init();
            _lateSystems.Init();
        }

        private void DestroySystem(EcsSystems system)
        {
            system?.Destroy();
            system = null;
        }

        private void DestroyWorld()
        {
            _world?.Destroy();
            _world = null;
        }
    }
}
