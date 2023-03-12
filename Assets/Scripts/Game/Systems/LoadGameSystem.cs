using Clicker.Core;
using Clicker.Core.Saves;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public class LoadGameSystem : IEcsInitSystem
    {
        private readonly GameWorld _world;
        private readonly SaveSystem _saveSystem;

        public LoadGameSystem(GameWorld world, SaveSystem saveSystem)
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
