using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public class IncomeSystem : IEcsRunSystem
    {
        private readonly GameWorld _world;

        private readonly EcsFilter<Income> _incomeFilter;

        public IncomeSystem(GameWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var i in _incomeFilter)
            {
                ref var income = ref _incomeFilter.Get1(i);

                _world.State.Balance += income.Value;

                var updateEntity = _world.NewEntity();
                ref var updateInfo = ref updateEntity.Get<BalanceUpdate>();

                _incomeFilter.GetEntity(i).Del<Income>();
            }
        }
    }
}
