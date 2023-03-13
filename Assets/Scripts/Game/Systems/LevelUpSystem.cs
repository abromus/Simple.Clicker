using Clicker.Core.World;
using Clicker.Game.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public sealed class LevelUpSystem : IEcsRunSystem
    {
        private readonly IWorld _world;

        private readonly EcsFilter<LevelUp> _levelUpFilter;

        public LevelUpSystem(IWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                ref var levelUp = ref _levelUpFilter.Get1(i);

                var defaultLevel = 0;
                var initLevel = levelUp.Level != defaultLevel ? levelUp.Level : defaultLevel;

                if (!_world.State.BusinessProgress.ContainsKey(levelUp.Id))
                    _world.State.BusinessProgress.Add(levelUp.Id, initLevel);

                _world.State.Balance -= levelUp.Price;
                _world.State.BusinessProgress[levelUp.Id]++;

                var updateEntity = _world.NewEntity();

                ref var updateLevelInfo = ref updateEntity.Get<LevelUpdate>();
                updateLevelInfo.Id = levelUp.Id;

                ref var updateBalanceInfo = ref updateEntity.Get<BalanceUpdate>();

                _levelUpFilter.GetEntity(i).Del<LevelUp>();
            }
        }
    }
}
