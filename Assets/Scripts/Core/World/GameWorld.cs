using Clicker.Core.Services;
using Leopotam.Ecs;
using SimpleJSON;

namespace Clicker.Core.World
{
    public sealed class GameWorld : EcsWorld, IWorld
    {
        private readonly ISaveSystem _saveSystem;

        private readonly IState _state;

        public IState State => _state;

        public float DeltaTime { get; set; }

        public GameWorld(ISaveSystem saveSystem) : base()
        {
            _saveSystem = saveSystem;

            _state = new State();

            _saveSystem.AddObserver(GameWorldKeys.GameWorld, ToSave);
        }

        public override void Destroy()
        {
            base.Destroy();

            _saveSystem.RemoveObserver(GameWorldKeys.GameWorld);
        }

        public void LoadSave(JSONNode json)
        {
            _state.FromJson(json[GameWorldKeys.State]);
        }

        private JSONObject ToSave(JSONObject json)
        {
            json[GameWorldKeys.State] = _state.ToJson();

            return json;
        }

        private sealed class GameWorldKeys
        {
            public const string GameWorld = nameof(GameWorld);
            public const string State = nameof(State);
        }
    }
}
