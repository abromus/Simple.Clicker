using Clicker.Core;

namespace Clicker.Game
{
    public interface IScreen
    {
        public ScreenType ScreenType { get; }

        public void Init(GameManagement game);
    }
}
