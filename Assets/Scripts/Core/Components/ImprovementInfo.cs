using System;

namespace Clicker.Core
{
    [Serializable]
    public class ImprovementInfo
    {
        public int BusinessId;
        public int ImprovementId;

        public ImprovementInfo(int businessId, int improvementId)
        {
            BusinessId = businessId;
            ImprovementId = improvementId;
        }
    }
}
