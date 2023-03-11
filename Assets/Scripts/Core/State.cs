using System.Collections.Generic;

namespace Clicker.Core
{
    public class State
    {
        public float Balance;
        public Dictionary<int, int> BusinessProgress;
        public List<ImprovementInfo> Improvements;

        public State()
        {
            Balance = 0f;
            BusinessProgress = new Dictionary<int, int>();
            Improvements = new List<ImprovementInfo>();
        }
    }
}
