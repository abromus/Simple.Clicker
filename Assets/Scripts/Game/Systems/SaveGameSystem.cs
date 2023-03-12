using Clicker.Core.Saves;
using Clicker.Core.Saves.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public class SaveGameSystem : IEcsRunSystem
    {
        private readonly SaveSystem _saveSystem;

        private readonly EcsFilter<Save> _saveFilter;

        public SaveGameSystem(SaveSystem saveSystem)
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
