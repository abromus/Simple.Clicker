using UnityEngine;

namespace Clicker.UI
{
    public class BusinessFactory : MonoBehaviour
    {
        [SerializeField] private BusinessView _businessPrefab;
        [SerializeField] private Transform _businessContainer;

        public BusinessView Create()
        {
            var business = Instantiate(_businessPrefab, _businessContainer);

            return business;
        }
    }
}
