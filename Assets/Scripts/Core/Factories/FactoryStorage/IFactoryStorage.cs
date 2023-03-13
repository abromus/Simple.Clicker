using System.Collections.Generic;
using Clicker.Core.Services;

namespace Clicker.Core.Factories
{
    public interface IFactoryStorage : IService
    {
        public ISavePathProviderFactory SavePathProviderFactory { get; }

        public IReadOnlyList<IUiFactory> UiFactories { get; }
    }
}
