using System.Collections.Generic;
using SimpleJSON;

namespace Clicker.Core
{
    public class State
    {
        public float Balance;
        public Dictionary<int, int> BusinessProgress;
        public Dictionary<int, float> Progress;
        public List<ImprovementInfo> Improvements;

        public State()
        {
            Balance = 0f;
            BusinessProgress = new Dictionary<int, int>();
            Progress = new Dictionary<int, float>();
            Improvements = new List<ImprovementInfo>();
        }

        public JSONObject ToJson()
        {
            var json = new JSONObject();
            json = SaveBalance(json);
            json = SaveBusinessProgress(json);
            json = SaveProgress(json);
            json = SaveImprovements(json);

            return json;
        }

        public void FromJson(JSONNode json)
        {
            LoadBalance(json);
            LoadBusinessProgress(json);
            LoadProgress(json);
            LoadImprovements(json);
        }

        #region Save
        private JSONObject SaveBalance(JSONObject json)
        {
            json[Keys.Balance] = Balance;

            return json;
        }

        private JSONObject SaveBusinessProgress(JSONObject json)
        {
            var businessProgressArray = new JSONArray();
            foreach (var item in BusinessProgress)
            {
                var businessObject = new JSONObject();
                businessObject[Keys.BusinessId] = item.Key;
                businessObject[Keys.Level] = item.Value;
                businessProgressArray.Add(businessObject);
            }

            json[Keys.BusinessProgress] = businessProgressArray;

            return json;
        }

        private JSONObject SaveProgress(JSONObject json)
        {
            var progressArray = new JSONArray();
            foreach (var item in Progress)
            {
                var progressObject = new JSONObject();
                progressObject[Keys.ProgressId] = item.Key;
                progressObject[Keys.ProgressValue] = item.Value;
                progressArray.Add(progressObject);
            }

            json[Keys.Progress] = progressArray;

            return json;
        }

        private JSONObject SaveImprovements(JSONObject json)
        {
            var improvementsArray = new JSONArray();
            foreach (var item in Improvements)
            {
                var improvementObject = new JSONObject();
                improvementObject[Keys.BusinessId] = item.BusinessId;
                improvementObject[Keys.ImprovementId] = item.ImprovementId;
                improvementsArray.Add(improvementObject);
            }

            json[Keys.Improvements] = improvementsArray;

            return json;
        }
        #endregion

        #region Load
        private void LoadBalance(JSONNode json)
        {
            Balance = json[Keys.Balance];
        }

        private void LoadBusinessProgress(JSONNode json)
        {
            var businessProgressArray = json[Keys.BusinessProgress].AsArray;
            if (businessProgressArray != null)
            {
                for (int i = 0; i < businessProgressArray.Count; i++)
                {
                    var businessId = businessProgressArray[i][Keys.BusinessId].AsInt;
                    var level = businessProgressArray[i][Keys.Level].AsInt;

                    BusinessProgress.Add(businessId, level);
                }
            }
        }

        private void LoadProgress(JSONNode json)
        {
            var progressArray = json[Keys.Progress].AsArray;
            if (progressArray != null)
            {
                for (int i = 0; i < progressArray.Count; i++)
                {
                    var progressId = progressArray[i][Keys.ProgressId].AsInt;
                    var value = progressArray[i][Keys.ProgressValue].AsFloat;

                    Progress.Add(progressId, value);
                }
            }
        }

        private void LoadImprovements(JSONNode json)
        {
            var improvementsArray = json[Keys.Improvements].AsArray;
            if (improvementsArray != null)
            {
                for (int i = 0; i < improvementsArray.Count; i++)
                {
                    var businessId = improvementsArray[i][Keys.BusinessId];
                    var improvementId = improvementsArray[i][Keys.ImprovementId];

                    Improvements.Add(new ImprovementInfo(businessId, improvementId));
                }
            }
        }
        #endregion

        private class Keys
        {
            public const string Balance = nameof(Balance);
            public const string BusinessId = nameof(BusinessId);
            public const string BusinessProgress = nameof(BusinessProgress);
            public const string ImprovementId = nameof(ImprovementId);
            public const string Improvements = nameof(Improvements);
            public const string Level = nameof(Level);
            public const string Progress = nameof(Progress);
            public const string ProgressId = nameof(ProgressId);
            public const string ProgressValue = nameof(ProgressValue);
        }
    }
}
