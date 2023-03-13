using UnityEngine;

namespace Clicker.Core.Services
{
    public interface IScreenSystem : IService
    {
        public void Init(IGameManagement game, Transform transform);

        public void ShowGame();
    }
}
