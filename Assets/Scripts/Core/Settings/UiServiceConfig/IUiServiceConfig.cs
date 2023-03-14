using System.Collections.Generic;
using Clicker.Core.Services;

namespace Clicker.Core.Settings
{
    public interface IUiServiceConfig : IUiConfig
    {
        public IReadOnlyList<IUiService> UiServices { get; }
    }
}
