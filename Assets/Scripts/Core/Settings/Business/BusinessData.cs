using System;
using System.Collections.Generic;

namespace Clicker.Core
{
    [Serializable]
    public class BusinessData
    {
        public string Title;
        public bool IsBought;
        public float DelayIncome;
        public int BasePrice;
        public int BaseIncome;

        public List<ImprovementData> Improvements;
    }
}
