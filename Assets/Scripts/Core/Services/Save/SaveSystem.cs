using System.Collections.Generic;
using SimpleJSON;

namespace Clicker.Core.Saves
{
    public class SaveSystem
    {
        private readonly Dictionary<string, SaveHandler> _observers = new Dictionary<string, SaveHandler>();
        private readonly SaveManager _saveManager;

        public delegate JSONObject SaveHandler(JSONObject json);

        public SaveSystem()
        {
            _saveManager = new SaveManager();
        }

        public void AddObserver(string key, SaveHandler observer)
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

        public JSONNode GetJson()
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
