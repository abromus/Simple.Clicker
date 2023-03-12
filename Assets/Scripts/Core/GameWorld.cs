using Clicker.Core.Saves;
using Clicker.Game;
using Leopotam.Ecs;
using SimpleJSON;

namespace Clicker.Core
{
    public class GameWorld : EcsWorld
    {
        private readonly ScreenSystem _screenSystem;
        private readonly SaveSystem _saveSystem;

        private readonly State _state;

        public ScreenSystem ScreenSystem => _screenSystem;

        public State State => _state;

        public float DeltaTime { get; set; }

        public GameWorld(ScreenSystem screenSystem, SaveSystem saveSystem) : base()
        {
            _screenSystem = screenSystem;
            _saveSystem = saveSystem;

            _state = new State();

            _saveSystem.AddObserver(nameof(GameWorld), ToSave);
        }

        public override void Destroy()
        {
            base.Destroy();

            _saveSystem.RemoveObserver(nameof(GameScreen));
        }

        public void LoadSave(JSONNode json)
        {
            _state.FromJson(json[nameof(State)]);
        }

        private JSONObject ToSave(JSONObject json)
        {
            json[nameof(State)] = _state.ToJson();

            return json;
        }
    }
}
