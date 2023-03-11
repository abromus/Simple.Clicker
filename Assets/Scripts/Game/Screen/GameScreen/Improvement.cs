using System.Linq;
using Clicker.Core;
using Clicker.Game.Components;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Improvement : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _info;

    private GameWorld _world;
    private string _title;
    private int _price;
    private float _incomeFactor;
    private int _businessId;
    private int _improvementId;

    private float _defaultIncomeFactor = 0f;

    private bool _isBought;

    public int ImprovementId => _improvementId;

    public float IncomeFactor => _isBought ? _incomeFactor : _defaultIncomeFactor;

    public void Init(GameWorld world, ImprovementData config, int businessId, int improvementId)
    {
        _world = world;
        _title = config.Title;
        _price = config.Price;
        _incomeFactor = config.IncomeFactor;
        _businessId = businessId;
        _improvementId = improvementId;

        UpdateInfo();

        _button.OnClickAsObservable().Subscribe(_ => Buy()).AddTo(this);
    }

    public void UpdateInfo()
    {
        var info = _world.State.Improvements.FirstOrDefault(info => info.BusinessId == _businessId && info.ImprovementId == _improvementId);

        _isBought = info != null && info.IsBought;
        _button.interactable = !_isBought;

        var percents = 100f;
        var state = _isBought ? "Куплено" : $"Цена: {_price}$";
        _info.text = $"{_title}\nДоход: +{_incomeFactor * percents}%\n{state}";
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
