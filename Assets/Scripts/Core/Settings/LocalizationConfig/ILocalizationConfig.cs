using System.Collections.Generic;

namespace Clicker.Core.Settings
{
    public interface ILocalizationConfig : IUiConfig
    {
        public IReadOnlyList<LocalizationData> LocalizationData { get; }
    }
}
