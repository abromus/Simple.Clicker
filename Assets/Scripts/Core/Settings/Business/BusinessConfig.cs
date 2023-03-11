using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Core
{
    [CreateAssetMenu(fileName = "BusinessConfig", menuName = "Settings/BusinessConfig")]
    public class BusinessConfig : ScriptableObject
    {
        [SerializeField] private List<BusinessData> _businessData;

        public IReadOnlyList<BusinessData> BusinessData => _businessData;
    }
}
