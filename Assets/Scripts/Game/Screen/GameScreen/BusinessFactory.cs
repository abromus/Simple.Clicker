using Clicker.Core;
using UnityEngine;

namespace Clicker.UI
{
    public class BusinessFactory : MonoBehaviour
    {
        [SerializeField] private BusinessView _businessPrefab;
        [SerializeField] private Transform _businessContainer;

        public BusinessView Create(GameManagement game, BusinessData businessData, ImprovementFactory improvementFactory, int businessId)
        {
            var business = Instantiate(_businessPrefab, _businessContainer);

            business.Init(game ,businessData, improvementFactory, businessId);

            return business;
        }
    }
}
