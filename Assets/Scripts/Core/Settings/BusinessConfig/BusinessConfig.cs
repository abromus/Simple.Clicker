using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "BusinessConfig", menuName = "Settings/BusinessConfig")]
    public sealed class BusinessConfig : ScriptableObject, IBusinessConfig
    {
        [SerializeField] private List<BusinessData> _businessData;

        public IReadOnlyList<BusinessData> BusinessData => _businessData;
    }
}
