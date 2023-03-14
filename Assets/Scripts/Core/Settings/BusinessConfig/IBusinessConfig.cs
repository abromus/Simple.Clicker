using System.Collections.Generic;

namespace Clicker.Core.Settings
{
    public interface IBusinessConfig : IUiConfig
    {
        public IReadOnlyList<BusinessData> BusinessData { get; }
    }
}
