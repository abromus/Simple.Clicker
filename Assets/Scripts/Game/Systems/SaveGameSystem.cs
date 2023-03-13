using Clicker.Core.Components;
using Clicker.Core.Services;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public sealed class SaveGameSystem : IEcsRunSystem
    {
        private readonly ISaveSystem _saveSystem;

        private readonly EcsFilter<Save> _saveFilter;

        public SaveGameSystem(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void Run()
        {
            foreach (var i in _saveFilter)
            {
                _saveSystem.Save();

                _saveFilter.GetEntity(i).Del<Save>();
            }
        }
    }
}
