using Clicker.Core.Services;
using Clicker.Core.World;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public sealed class LoadGameSystem : IEcsInitSystem
    {
        private readonly IWorld _world;
        private readonly ISaveSystem _saveSystem;

        public LoadGameSystem(IWorld world, ISaveSystem saveSystem)
        {
            _world = world;
            _saveSystem = saveSystem;
        }

        public void Init()
        {
            var save = _saveSystem.GetSave();

            if (save == null)
                return;

            _world.LoadSave(save[nameof(GameWorld)]);
        }
    }
}
