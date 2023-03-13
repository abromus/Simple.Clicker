using Clicker.Game.Screens;
using UnityEngine;

namespace Clicker.Core.Factories
{
    public sealed class BusinessFactory : UiFactory
    {
        [SerializeField] private BusinessView _businessPrefab;

        public override UiFactoryType UiFactoryType => UiFactoryType.BusinessFactory;

        public override MonoBehaviour Create(BaseOptions options, Transform container)
        {
            var business = Instantiate(_businessPrefab, container);

            business.Init(options as BusinessViewOptions);

            return business;
        }
    }
}
