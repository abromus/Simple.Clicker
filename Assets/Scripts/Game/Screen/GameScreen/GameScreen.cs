using System.Collections.Generic;
using Clicker.Core;
using Clicker.Game.Components;
using Clicker.UI;
using Leopotam.Ecs;
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

        public const string GameKey = "game_key";

        public override ScreenType ScreenType => ScreenType.Game;

        public override void Init(Dictionary<string, object> data)
        {
            _game = (GameManagement)data[GameKey];

            _businessViews = new List<BusinessView>();

            _subscription = new CompositeDisposable();

            var balanceUpdateFilterType = typeof(EcsFilter<BalanceUpdate>);
            _balanceUpdateFilter = _game.World.GetFilter(balanceUpdateFilterType) as EcsFilter<BalanceUpdate>;

            _game.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(_subscription);

            CreateBusiness();
            UpdateBalance();
        }

        public override void Show(Dictionary<string, object> data)
        {
            base.Show(data);
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
            _balance.text = $"{_game.World.State.Balance}$";
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
