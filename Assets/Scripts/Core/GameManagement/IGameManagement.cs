using System;
using Clicker.Core.World;
using Clicker.Core.Services;
using Clicker.Core.Settings;

namespace Clicker.Core
{
    public interface IGameManagement
    {
        public IConfigStorage ConfigStorage { get; }

        public IServiceStorage ServiceStorage { get; }

        public IObservable<Null> ViewUpdated { get; }

        public IWorld World { get; }

        public void Destroy();

        public void LateTick();

        public void Tick(float deltaTime);

        public void ViewUpdate();
    }
}
