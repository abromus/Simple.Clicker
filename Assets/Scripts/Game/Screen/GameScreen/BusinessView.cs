using Clicker.Core;
using Clicker.Core.Services;
using Clicker.Core.World;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Game.Screens
{
    public sealed class BusinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Slider _progress;
        [SerializeField] private Management _management;

        private IWorld _world;
        private ILocalizationSystem _localizationSystem;

        private int _id;
        private float _delayIncome;

        private EcsEntity _timerEntity;

        private readonly int _lockLevel = 0;
        private readonly float _defaultProgress = 0f;

        private EcsFilter<LevelUpdate> _levelUpdateFilter;
        private EcsFilter<Timer> _timerFilter;

        public void Init(BusinessViewOptions options)
        {
            _world = options.GameOptions.World;
            _localizationSystem = options.GameOptions.LocalizationSystem;
            _id = options.BusinessId;

            _title.text = _localizationSystem.Get(options.BusinessData.Title);
            _delayIncome = options.BusinessData.DelayIncome;

            _management.Init(options);

            _progress.value = _world.State.Progress.ContainsKey(_id) ? _world.State.Progress[_id] : _defaultProgress;

            options.GameOptions.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(this);

            CreateFilters();

            CreateTimer();
        }

        private void CreateFilters()
        {
            _levelUpdateFilter = _world.CreateFilter<EcsFilter<LevelUpdate>>();
            _timerFilter = _world.CreateFilter<EcsFilter<Timer>>();
        }

        private void CollectIncome()
        {
            var incomeEntity = _world.NewEntity();
            ref var income = ref incomeEntity.Get<Income>();
            income.Value = _management.Income;
        }

        private void OnViewUpdated()
        {
            OnLevelUpdate();
            OnTimerUpdate();
        }

        private void OnLevelUpdate()
        {
            foreach (var i in _levelUpdateFilter)
            {
                ref var levelUpdate = ref _levelUpdateFilter.Get1(i);

                if (levelUpdate.Id != _id)
                    continue;

                _management.UpdateInfo();

                CreateTimer();

                _levelUpdateFilter.GetEntity(i).Del<LevelUpdate>();
            }
        }

        private void OnTimerUpdate()
        {
            foreach (var i in _timerFilter)
            {
                ref var timer = ref _timerFilter.Get1(i);

                if (timer.Id != _id)
                    continue;

                if (timer.Time <= 0f)
                {
                    CollectIncome();
                    timer.Time = _delayIncome;
                }

                UpdateProgress(timer);
            }
        }

        private void CreateTimer()
        {
            if (_management.Level <= _lockLevel)
                return;

            _timerEntity = _timerEntity.IsNull() ? _world.NewEntity() : _timerEntity;
            ref var timer = ref _timerEntity.Get<Timer>();
            timer.Id = _id;
            timer.Time = _delayIncome * (1 - _progress.value);
        }

        private void UpdateProgress(Timer timer)
        {
            var progress = Mathf.Clamp01((_delayIncome - timer.Time) / _delayIncome);
            _world.State.Progress[_id] = progress;

            _progress.value = progress;
        }
    }
}
