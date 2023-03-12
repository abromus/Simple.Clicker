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
    public class Improvement : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _info;

        private GameWorld _world;
        private LocalizationSystem _localizationSystem;
        private string _title;
        private int _price;
        private float _incomeFactor;
        private int _businessId;
        private int _improvementId;

        private bool _isBought;

        private readonly float _defaultIncomeFactor = 0f;

        public int ImprovementId => _improvementId;

        public float IncomeFactor => _isBought ? _incomeFactor : _defaultIncomeFactor;

        public void Init(GameWorld world, LocalizationSystem localizationSystem, ImprovementData config, int businessId, int improvementId)
        {
            _world = world;
            _localizationSystem = localizationSystem;
            _title = string.Format(_localizationSystem.Get(config.Title), _businessId, _improvementId);
            _price = config.Price;
            _incomeFactor = config.IncomeFactor;
            _businessId = businessId;
            _improvementId = improvementId;

            UpdateInfo();

            _button.OnClickAsObservable().Subscribe(_ => Buy()).AddTo(this);
        }

        public void UpdateInfo()
        {
            var info = _world.State.Improvements.FirstOrDefault(info =>
                info.BusinessId == _businessId
                && info.ImprovementId == _improvementId);

            _isBought = info != null;
            _button.interactable = !_isBought;

            var percents = 100f;
            var income = string.Format(_localizationSystem.Get(LocalizationKeys.ImprovementIncome), _incomeFactor * percents);
            var state = _isBought
                ? _localizationSystem.Get(LocalizationKeys.IsBought)
                : string.Format(_localizationSystem.Get(LocalizationKeys.Price), _price);

            _info.text = string.Format(_localizationSystem.Get(LocalizationKeys.ImprovementInfo), _title, income, state);
        }

        private void Buy()
        {
            if (_world.State.Balance < _price)
                return;

            var improvementEntity = _world.NewEntity();
            ref var purchase = ref improvementEntity.Get<ImprovementPurchase>();
            purchase.BusinessId = _businessId;
            purchase.ImprovementId = _improvementId;
            purchase.Price = _price;
        }
    }
}
