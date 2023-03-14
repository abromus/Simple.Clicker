using System.Collections.Generic;
using Clicker.Game.Screens;

namespace Clicker.Core.Settings
{
    public interface IScreenConfig : IUiConfig
    {
        public IReadOnlyList<Screen> Screens { get; }
    }
}
