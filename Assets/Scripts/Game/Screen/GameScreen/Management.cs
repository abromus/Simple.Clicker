using System;
using System.Collections.Generic;
using System.Linq;
using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Game
{
    public class Management : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelInfo;
        [SerializeField] private TMP_Text _incomeInfo;

        [Space]
        [SerializeField] private Button _levelUp;
        [SerializeField] private TMP_Text _levelUpInfo;

        [Space]
        [SerializeField] private Improvement _improvementPrefab;
        [SerializeField] private Transform _improvementContainer;

        private GameManagement _game;

        private int _basePrice;
        private int _baseIncome;
        private bool _isBought;
        private int _id;
        private int _level;

        private int _defaultLevel = 1;
        private int _lockLevel = 0;

        private float _income;

        private List<Improvement> _improvements;

        private EcsFilter<ImprovementUpdate> _improvementUpdateFilter;

        private int _levelUpPrice;

        public int Level => _level;

        public float Income => _income;

        public void Init(GameManagement game, BusinessData businessData, ImprovementFactory improvementFactory, int businessId)
        {
            _game = game;
            _basePrice = businessData.BasePrice;
            _baseIncome = businessData.BaseIncome;
            _isBought = businessData.IsBought;
            _id = businessId;

            InitImprovements(businessData, improvementFactory);

            UpdateInfo();

            var improvementUpdateFilterType = typeof(EcsFilter<ImprovementUpdate>);
            _improvementUpdateFilter = _game.World.GetFilter(improvementUpdateFilterType) as EcsFilter<ImprovementUpdate>;

            _levelUp.OnClickAsObservable().Subscribe(_ => LevelUp()).AddTo(this);

            game.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(this);
        }

        public void UpdateInfo()
        {
            UpdateLevel();
            CalculateIncome();
            UpdateLevelInfo();
            UpdateIncomeInfo();
            UpdateLevelUpInfo();
        }

        private void LevelUp()
        {
            if (_game.World.State.Balance < _levelUpPrice)
                return;

            var levelUpEntity = _game.World.NewEntity();
            ref var levelUp = ref levelUpEntity.Get<LevelUp>();
            levelUp.Id = _id;
            levelUp.Price = _levelUpPrice;

            if (_isBought)
                levelUp.Level = _level;
        }

        private void InitImprovements(BusinessData businessData, ImprovementFactory improvementFactory)
        {
            _improvements = new List<Improvement>();
            var improvementId = 0;

            foreach (var improvementData in businessData.Improvements)
            {
                improvementId++;

                var improvement = improvementFactory.Create(_game.World, _game.LocalizationSystem, improvementData, _improvementContainer, _id, improvementId);

                _improvements.Add(improvement);
            }
        }

        private void OnViewUpdated()
        {
            foreach (var i in _improvementUpdateFilter)
            {
                ref var improvementUpdate = ref _improvementUpdateFilter.Get1(i);

                if (improvementUpdate.BusinessId != _id)
                    continue;

                var id = improvementUpdate.ImprovementId;
                var improvement = _improvements.FirstOrDefault(improvement => improvement.ImprovementId == id);

                improvement.UpdateInfo();

                UpdateInfo();

                _improvementUpdateFilter.GetEntity(i).Del<ImprovementUpdate>();
            }
        }

        private void UpdateLevelInfo()
        {
            _levelInfo.text = string.Format(_game.LocalizationSystem.Get(LocalizationKeys.LevelInfo), _level);
        }

        private void UpdateIncomeInfo()
        {
            _incomeInfo.text = string.Format(_game.LocalizationSystem.Get(LocalizationKeys.IncomeInfo), _income);
        }

        private void UpdateLevelUpInfo()
        {
            _levelUpPrice = _basePrice * (_level + 1);

            var price = string.Format(_game.LocalizationSystem.Get(LocalizationKeys.Price), _levelUpPrice);
            _levelUpInfo.text = string.Format(_game.LocalizationSystem.Get(LocalizationKeys.LevelUp), price);
        }

        private void UpdateLevel()
        {
            _level = _game.World.State.BusinessProgress.ContainsKey(_id)
                ? _game.World.State.BusinessProgress[_id]
                : _isBought
                    ? _defaultLevel
                    : _lockLevel;
        }

        private void CalculateIncome()
        {
            var incomeFactor = 1f;
            foreach (var improvement in _improvements)
                incomeFactor += improvement.IncomeFactor;
            _income = _level * _baseIncome * incomeFactor;
        }
    }
}
