using Clicker.Core.World;
using Clicker.Game.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public sealed class TimerSystem : IEcsRunSystem
    {
        private readonly IWorld _world;

        private readonly EcsFilter<Timer> _timerFilter;

        public TimerSystem(IWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var i in _timerFilter)
            {
                ref var timer = ref _timerFilter.Get1(i);
                timer.Time -= _world.DeltaTime;
            }
        }
    }
}
