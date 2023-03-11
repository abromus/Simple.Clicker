using System;

namespace Clicker.Core
{
    [Serializable]
    public class ImprovementInfo
    {
        public int BusinessId;
        public int ImprovementId;
        public bool IsBought;

        public ImprovementInfo(int businessId, int improvementId, bool isBought)
        {
            BusinessId = businessId;
            ImprovementId = improvementId;
            IsBought = isBought;
        }
    }
}
