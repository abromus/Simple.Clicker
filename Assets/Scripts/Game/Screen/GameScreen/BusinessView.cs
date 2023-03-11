using Clicker.Core;
using Clicker.Game.Components;
using DG.Tweening;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.UI
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Slider _progress;
        [SerializeField] private Management _management;

        private readonly int _lockLevel = 0;

        private GameManagement _game;
        private float _delayIncome;
        private int _id;

        private EcsFilter<LevelUpdate> _levelUpdateFilter;
        private Tween _tween;

        public void Init(GameManagement game, BusinessData businessData, ImprovementFactory improvementFactory, int id)
        {
            _game = game;
            _id = id;
            _title.text = businessData.Title;
            _delayIncome = businessData.DelayIncome;

            _management.Init(game, businessData, improvementFactory, id);

            _game.ViewUpdated.Subscribe(_ => OnViewUpdated()).AddTo(this);

            var levelUpdateFilterType = typeof(EcsFilter<LevelUpdate>);
            _levelUpdateFilter = _game.World.GetFilter(levelUpdateFilterType) as EcsFilter<LevelUpdate>;

            RunProgress();
        }

        private void RunProgress()
        {
            if (_management.Level <= _lockLevel || _tween != null)
                return;

            var endValue = 1f;

            _tween = _progress.DOValue(endValue, _delayIncome).OnComplete(CollectIncome);
        }

        private void CollectIncome()
        {
            var incomeEntity = _game.World.NewEntity();
            ref var income = ref incomeEntity.Get<Income>();
            income.Value = _management.Income;

            _tween?.Kill();
            _tween = null;

            var startValue = 0f;
            _progress.value = startValue;

            RunProgress();
        }

        private void OnViewUpdated()
        {
            foreach (var i in _levelUpdateFilter)
            {
                ref var levelUpdate = ref _levelUpdateFilter.Get1(i);

                if (levelUpdate.Id != _id)
                    continue;

                _management.UpdateInfo();

                RunProgress();

                _levelUpdateFilter.GetEntity(i).Del<LevelUpdate>();
            }
        }
    }
}
