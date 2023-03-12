using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public class ImprovementPurchaseSystem : IEcsRunSystem
    {
        private readonly GameWorld _world;

        private readonly EcsFilter<ImprovementPurchase> _improvementPurchaseFilter;

        public ImprovementPurchaseSystem(GameWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var i in _improvementPurchaseFilter)
            {
                ref var improvementPurchase = ref _improvementPurchaseFilter.Get1(i);

                _world.State.Balance -= improvementPurchase.Price;

                _world.State.Improvements.Add(new ImprovementInfo(
                    improvementPurchase.BusinessId,
                    improvementPurchase.ImprovementId));

                var updateEntity = _world.NewEntity();
                ref var updateInfo = ref updateEntity.Get<ImprovementUpdate>();
                updateInfo.BusinessId = improvementPurchase.BusinessId;
                updateInfo.ImprovementId = improvementPurchase.ImprovementId;

                _improvementPurchaseFilter.GetEntity(i).Del<ImprovementPurchase>();
            }
        }
    }
}
