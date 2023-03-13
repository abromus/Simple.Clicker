using System;
using System.Collections.Generic;
using SimpleJSON;

namespace Clicker.Core.Services
{
    public sealed class SaveSystem : ISaveSystem
    {
        private readonly ISaveManager _saveManager;

        private readonly Dictionary<string, Func<JSONObject, JSONObject>> _observers = new Dictionary<string, Func<JSONObject, JSONObject>>();

        public SaveSystem(ISaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        public void AddObserver(string key, Func<JSONObject, JSONObject> observer)
        {
            _observers.Add(key, observer);
        }

        public void RemoveObserver(string key)
        {
            _observers.Remove(key);
        }

        public JSONNode GetSave()
        {
            var save = _saveManager.Load();
            var json = save != null? JSONNode.Parse(save) : null;

            return json;
        }

        public void Save()
        {
            var json = GetJson();

            _saveManager.Save(json.ToString());
        }

        private JSONNode GetJson()
        {
            var mainJson = new JSONObject();

            foreach (var observer in _observers)
            {
                var json = observer.Value.Invoke(new JSONObject());
                mainJson[observer.Key] = json;
            }

            return mainJson;
        }
    }
}
