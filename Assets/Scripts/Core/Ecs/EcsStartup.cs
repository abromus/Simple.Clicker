using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Core
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private StaticData _configuration;
        [SerializeField] private SceneData _sceneData;

        private EcsWorld _world;
        private EcsSystems _systems;

        public EcsWorld World => _world;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            var runtimeData = new RuntimeData();

            PrepareSystems();

            InjectData(_configuration, _sceneData, runtimeData);

            InitSystems();

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }

        private void PrepareSystems()
        {
        }

        private void InjectData(StaticData configuration, SceneData sceneData, RuntimeData runtimeData)
        {
            _systems
                .Inject(configuration)
                .Inject(sceneData)
                .Inject(runtimeData);
        }

        private void InitSystems()
        {
            _systems.Init();
        }
    }
}
