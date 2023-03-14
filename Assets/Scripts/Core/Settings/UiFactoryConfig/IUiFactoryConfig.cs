using System.Collections.Generic;
using Clicker.Core.Factories;

namespace Clicker.Core.Settings
{
    public interface IUiFactoryConfig : IUiConfig
    {
        public IReadOnlyList<IUiFactory> UiFactories { get; }
    }
}
