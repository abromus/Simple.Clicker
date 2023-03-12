using System.Collections.Generic;
using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;
using SimpleJSON;
using TMPro;
using UniRx;
using UnityEngine;

namespace Clicker.Game
{
    public class GameScreen : Screen
    {
        [SerializeField] private TMP_Text _balance;

        [SerializeField] private BusinessFactory _businessFactory;
        [SerializeField] private ImprovementFactory _improvementFactory;

        private GameManagement _game;

        private List<BusinessView> _businessViews;

        private CompositeDisposable _subscription;

        private EcsFilter<BalanceUpdate> _balanceUpdateFilter;

        public override ScreenType ScreenType => ScreenType.Game;

        public override void Init(GameManagement game)
        {
            base.Init(game);

            _game = game;

            _businessViews = new List<BusinessView>();

            _subscription = new CompositeDisposable();

            var balanceUpdateFilterType = typeof(EcsFilter<BalanceUpdate>);
            _balanceUpdateFilter = _game.World.GetFilter(balanceUpdateFilterType) as EcsFilter<BalanceUpdate>;

            _game.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(_subscription);

            CreateBusiness();
            UpdateBalance();
        }

        private void OnViewUpdated()
        {
            foreach (var i in _balanceUpdateFilter)
            {
                UpdateBalance();

                _balanceUpdateFilter.GetEntity(i).Del<BalanceUpdate>();
            }
        }

        private void UpdateBalance()
        {
            _balance.text = string.Format(_game.LocalizationSystem.Get(LocalizationKeys.Balance), _game.World.State.Balance);
        }

        private void CreateBusiness()
        {
            var businessConfig = _game.ConfigData.BusinessConfig.BusinessData;
            var businessId = 0;

            foreach (var businessData in businessConfig)
            {
                businessId++;

                var businessView = _businessFactory.Create(_game, businessData, _improvementFactory, businessId);
                _businessViews.Add(businessView);
            }
        }
    }
}
