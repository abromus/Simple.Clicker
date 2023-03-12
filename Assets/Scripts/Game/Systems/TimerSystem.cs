using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public class TimerSystem : IEcsRunSystem
    {
        private readonly GameWorld _world;

        private readonly EcsFilter<Timer> _timerFilter;

        public TimerSystem(GameWorld world)
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
