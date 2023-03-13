using System.Collections.Generic;
using System.Linq;
using Clicker.Core;
using Clicker.Core.Factories;
using Clicker.Core.Services;
using Clicker.Core.Settings;
using Clicker.Core.World;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Game.Screens
{
    public sealed class Management : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelInfo;
        [SerializeField] private TMP_Text _incomeInfo;

        [Space]
        [SerializeField] private Button _levelUp;
        [SerializeField] private TMP_Text _levelUpInfo;

        [Space]
        [SerializeField] private Improvement _improvementPrefab;
        [SerializeField] private Transform _improvementContainer;

        private BusinessViewOptions _options;

        private IWorld _world;
        private ILocalizationSystem _localizationSystem;

        private int _id;
        private int _basePrice;
        private int _baseIncome;
        private bool _isBought;

        private int _level;
        private int _levelUpPrice;
        private float _income;

        private List<Improvement> _improvements;

        private EcsFilter<ImprovementUpdate> _improvementUpdateFilter;


        public int Level => _level;

        public float Income => _income;

        public void Init(BusinessViewOptions options)
        {
            _options = options;
            _world = _options.GameOptions.World;
            _localizationSystem = _options.GameOptions.LocalizationSystem;
            _id = _options.BusinessId;
            _basePrice = _options.BusinessData.BasePrice;
            _baseIncome = _options.BusinessData.BaseIncome;
            _isBought = _options.BusinessData.IsBought;

            InitImprovements(_options.BusinessData);

            UpdateInfo();

            _improvementUpdateFilter = _world.CreateFilter<EcsFilter<ImprovementUpdate>>();

            _levelUp.OnClickAsObservable().Subscribe(_ => LevelUp()).AddTo(this);

            _options.GameOptions.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(this);
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
            if (_world.State.Balance < _levelUpPrice)
                return;

            var levelUpEntity = _world.NewEntity();
            ref var levelUp = ref levelUpEntity.Get<LevelUp>();
            levelUp.Id = _id;
            levelUp.Price = _levelUpPrice;

            if (_isBought)
                levelUp.Level = _level;
        }

        private void InitImprovements(BusinessData businessData)
        {
            _improvements = new List<Improvement>();

            var improvementId = 0;

            foreach (var improvementData in businessData.Improvements)
            {
                improvementId++;

                var options = new ImprovementOptions(
                    _world,
                    _localizationSystem,
                    improvementData,
                    _id,
                    improvementId);

                var improvementFactory = _options.GameOptions.UiFactories
                    .FirstOrDefault(factory => factory.UiFactoryType == UiFactoryType.ImprovementFactory);

                var improvement = improvementFactory.Create(options, _improvementContainer);

                _improvements.Add(improvement as Improvement);
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
            _levelInfo.text = _localizationSystem.Get(LocalizationKeys.LevelInfo, _level.ToString());
        }

        private void UpdateIncomeInfo()
        {
            _incomeInfo.text = _localizationSystem.Get(LocalizationKeys.IncomeInfo, _income.ToString());
        }

        private void UpdateLevelUpInfo()
        {
            _levelUpPrice = _basePrice * (_level + 1);

            var price = _localizationSystem.Get(LocalizationKeys.Price, _levelUpPrice.ToString());
            _levelUpInfo.text = _localizationSystem.Get(LocalizationKeys.LevelUp, price);
        }

        private void UpdateLevel()
        {
            var defaultLevel = 1;
            var lockLevel = 0;

            _level = _world.State.BusinessProgress.ContainsKey(_id)
                ? _world.State.BusinessProgress[_id]
                : _isBought
                    ? defaultLevel
                    : lockLevel;
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
