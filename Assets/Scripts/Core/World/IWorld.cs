using System;
using Leopotam.Ecs;
using SimpleJSON;

namespace Clicker.Core.World
{
    public interface IWorld
    {
        public IState State { get; }

        public float DeltaTime { get; set; }

        public void Destroy();

        public EcsFilter GetFilter(Type filterType, bool createIfNotExists = true);

        public void LoadSave(JSONNode json);

        public EcsEntity NewEntity();
    }
}
