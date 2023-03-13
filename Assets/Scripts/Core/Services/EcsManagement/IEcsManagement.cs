using Clicker.Core.World;

namespace Clicker.Core.Services
{
    public interface IEcsManagement : IService
    {
        public IWorld World { get; }

        public void Destroy();

        public void LateTick();

        public void Tick(float deltaTime);
    }
}
