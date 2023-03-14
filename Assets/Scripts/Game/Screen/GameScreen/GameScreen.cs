using System.Collections.Generic;
using System.Linq;
using Clicker.Core;
using Clicker.Core.Factories;
using Clicker.Core.Settings;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;

namespace Clicker.Game.Screens
{
    public sealed class GameScreen : Screen
    {
        [SerializeField] private TMP_Text _balance;

        [SerializeField] private Transform _businessContainer;

        private GameScreenOptions _options;

        private List<BusinessView> _businessViews;

        private CompositeDisposable _subscription;

        private EcsFilter<BalanceUpdate> _balanceUpdateFilter;

        public override ScreenType ScreenType => ScreenType.Game;

        public override void Init(BaseOptions options)
        {
            _options = options as GameScreenOptions;

            _businessViews = new List<BusinessView>();

            _subscription = new CompositeDisposable();

            _balanceUpdateFilter = _options.World.CreateFilter<EcsFilter<BalanceUpdate>>();

            CreateBusiness();
            UpdateBalance();

            _options.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(_subscription);
        }

        private void UpdateBalance()
        {
            _balance.text = _options.LocalizationSystem.Get(LocalizationKeys.Balance, _options.World.State.Balance.ToString());
        }

        private void CreateBusiness()
        {
            var businessConfig = _options.ConfigStorage.GetUiConfig<IBusinessConfig>().BusinessData;
            var businessId = 0;

            foreach (var businessData in businessConfig)
            {
                businessId++;

                var options = new BusinessViewOptions(
                    _options,
                    businessId,
                    businessData);

                var businessFactory = _options.UiFactories
                    .FirstOrDefault(factory => factory.UiFactoryType == UiFactoryType.BusinessFactory);

                var businessView = businessFactory.Create(options, _businessContainer) as BusinessView;

                _businessViews.Add(businessView);
            }
        }

        private void OnViewUpdated()
        {
            foreach (var i in _balanceUpdateFilter)
            {
                UpdateBalance();

                _balanceUpdateFilter.GetEntity(i).Del<BalanceUpdate>();
            }
        }
    }
}
