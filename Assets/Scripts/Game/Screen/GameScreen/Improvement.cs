using System.Linq;
using Clicker.Core.World;
using Clicker.Core.Services;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Game.Screens
{
    public sealed class Improvement : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _info;

        private IWorld _world;
        private ILocalizationSystem _localizationSystem;

        private string _title;
        private int _price;
        private float _incomeFactor;
        private int _businessId;
        private int _improvementId;

        private bool _isBought;

        private readonly float _defaultIncomeFactor = 0f;

        public int ImprovementId => _improvementId;

        public float IncomeFactor => _isBought ? _incomeFactor : _defaultIncomeFactor;

        public void Init(ImprovementOptions options)
        {
            _world = options.World;
            _price = options.ImprovementData.Price;
            _incomeFactor = options.ImprovementData.IncomeFactor;
            _businessId = options.BusinessId;
            _improvementId = options.ImprovementId;
            _localizationSystem = options.LocalizationSystem;
            _title = _localizationSystem.Get(options.ImprovementData.Title, _businessId.ToString(), _improvementId.ToString());

            _button.OnClickAsObservable().Subscribe(_ => Buy()).AddTo(this);

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            var info = _world.State.Improvements
                .FirstOrDefault(info => info.BusinessId == _businessId && info.ImprovementId == _improvementId);

            _isBought = info != null;
            _button.interactable = !_isBought;

            var percents = 100f;
            var incomeFactor = _incomeFactor * percents;
            var income = _localizationSystem.Get(LocalizationKeys.ImprovementIncome, incomeFactor.ToString());
            var state = _isBought
                ? _localizationSystem.Get(LocalizationKeys.IsBought)
                : _localizationSystem.Get(LocalizationKeys.Price, _price.ToString());

            _info.text = _localizationSystem.Get(LocalizationKeys.ImprovementInfo, _title, income, state);
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
