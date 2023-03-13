using System.Collections.Generic;
using Clicker.Core.Components;
using SimpleJSON;

namespace Clicker.Core.World
{
    public interface IState
    {
        public float Balance { get; set; }

        public Dictionary<int, int> BusinessProgress { get; set; }

        public Dictionary<int, float> Progress { get; set; }

        public List<ImprovementInfo> Improvements { get; set; }

        public void FromJson(JSONNode json);

        public JSONObject ToJson();
    }
}