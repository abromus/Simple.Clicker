using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Game
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Slider _progress;
        [SerializeField] private Management _management;

        private readonly int _lockLevel = 0;
        private readonly float _defaultProgress = 0f;

        private GameManagement _game;
        private LocalizationSystem _localizationSystem;

        private float _delayIncome;
        private int _id;

        private EcsEntity _timerEntity;

        private EcsFilter<LevelUpdate> _levelUpdateFilter;
        private EcsFilter<Timer> _timerFilter;

        public void Init(GameManagement game, BusinessData businessData, ImprovementFactory improvementFactory, int id)
        {
            _game = game;
            _localizationSystem = _game.LocalizationSystem;
            _id = id;

            _title.text = string.Format(_localizationSystem.Get(businessData.Title));
            _delayIncome = businessData.DelayIncome;

            _management.Init(game, businessData, improvementFactory, id);

            _game.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(this);

            var levelUpdateFilterType = typeof(EcsFilter<LevelUpdate>);
            _levelUpdateFilter = _game.World.GetFilter(levelUpdateFilterType) as EcsFilter<LevelUpdate>;

            var timerFilterType = typeof(EcsFilter<Timer>);
            _timerFilter = _game.World.GetFilter(timerFilterType) as EcsFilter<Timer>;

            _progress.value = _game.World.State.Progress.ContainsKey(_id) ? _game.World.State.Progress[_id] : _defaultProgress;

            CreateTimer();
        }

        private void CollectIncome()
        {
            var incomeEntity = _game.World.NewEntity();
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

                var progress = Mathf.Clamp01((_delayIncome - timer.Time) / _delayIncome);
                _game.World.State.Progress[_id] = progress;

                _progress.value = progress;
            }
        }

        private void CreateTimer()
        {
            if (_management.Level <= _lockLevel)
                return;

            _timerEntity = _timerEntity.IsNull() ? _game.World.NewEntity() : _timerEntity;
            ref var timer = ref _timerEntity.Get<Timer>();
            timer.Id = _id;
            timer.Time = _delayIncome * (1 - _progress.value);
        }
    }
}
