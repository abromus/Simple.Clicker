using System;
using SimpleJSON;

namespace Clicker.Core.Services
{
    public interface ISaveSystem : IService
    {
        public void AddObserver(string key, Func<JSONObject, JSONObject> observer);

        public JSONNode GetSave();

        public void RemoveObserver(string key);

        public void Save();
    }
}
