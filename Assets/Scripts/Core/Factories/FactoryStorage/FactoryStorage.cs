using System.Collections.Generic;
using Clicker.Core.Settings;

namespace Clicker.Core.Factories
{
    public sealed class FactoryStorage : IFactoryStorage
    {
        private readonly ISavePathProviderFactory _pathProviderFactory;
        private readonly IReadOnlyList<IUiFactory> _uiFactories;

        public ISavePathProviderFactory SavePathProviderFactory => _pathProviderFactory;

        public IReadOnlyList<IUiFactory> UiFactories => _uiFactories;

        public FactoryStorage(IUiFactoryConfig config)
        {
            _pathProviderFactory = new SavePathProviderFactory();

            _uiFactories = config.UiFactories;
        }
    }
}
