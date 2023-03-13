using System;

namespace Clicker.Core.Components
{
    [Serializable]
    public sealed class ImprovementInfo
    {
        public int BusinessId { get; }

        public int ImprovementId { get; }

        public ImprovementInfo(int businessId, int improvementId)
        {
            BusinessId = businessId;
            ImprovementId = improvementId;
        }
    }
}
