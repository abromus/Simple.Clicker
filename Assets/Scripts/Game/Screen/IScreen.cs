using System.Collections.Generic;

namespace Clicker.Game
{
    public interface IScreen
    {
        public ScreenType ScreenType { get; }

        public void Init(Dictionary<string, object> data);
        public void Show(Dictionary<string, object> data);
    }
}
