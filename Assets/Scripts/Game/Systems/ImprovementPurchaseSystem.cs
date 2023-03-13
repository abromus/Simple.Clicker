using Clicker.Core.Components;
using Clicker.Core.World;
using Clicker.Game.Components;
using Leopotam.Ecs;

namespace Clicker.Game.Systems
{
    public sealed class ImprovementPurchaseSystem : IEcsRunSystem
    {
        private readonly IWorld _world;

        private readonly EcsFilter<ImprovementPurchase> _improvementPurchaseFilter;

        public ImprovementPurchaseSystem(IWorld world)
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
                ref var improvementUpdateInfo = ref updateEntity.Get<ImprovementUpdate>();
                improvementUpdateInfo.BusinessId = improvementPurchase.BusinessId;
                improvementUpdateInfo.ImprovementId = improvementPurchase.ImprovementId;

                ref var balanceUpdateInfo = ref updateEntity.Get<BalanceUpdate>();

                _improvementPurchaseFilter.GetEntity(i).Del<ImprovementPurchase>();
            }
        }
    }
}
