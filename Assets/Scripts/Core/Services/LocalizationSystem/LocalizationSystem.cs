using System;
using System.Linq;
using Clicker.Core.Settings;

namespace Clicker.Core.Services
{
    public sealed class LocalizationSystem : ILocalizationSystem, IService
    {
        private readonly LocalizationConfig _config;

        public LocalizationSystem(LocalizationConfig localizationConfig)
        {
            _config = localizationConfig;
        }

        public string Get(string key)
        {
            return GetKey(key);
        }

        public string Get(string key, string value)
        {
            var localizeKey = GetKey(key);

            return localizeKey != null ? string.Format(localizeKey, value) : null;
        }

        public string Get(string key, string value1, string value2)
        {
            var localizeKey = GetKey(key);

            return localizeKey != null ? string.Format(localizeKey, value1, value2) : null;
        }

        public string Get(string key, string value1, string value2, string value3)
        {
            var localizeKey = GetKey(key);

            return localizeKey != null ? string.Format(localizeKey, value1, value2, value3) : null;
        }

        private string GetKey(string key)
        {
            var data = _config.LocalizationData.FirstOrDefault(item => item.Key == key);

            return data != null ? Trim(data.Value) : key;
        }

        private string Trim(string data)
        {
            return data
                .Replace(LocalizationTrimKeys.RN, Environment.NewLine)
                .Replace(LocalizationTrimKeys.R, Environment.NewLine)
                .Replace(LocalizationTrimKeys.N, Environment.NewLine);
        }

        private sealed class LocalizationTrimKeys
        {
            public const string RN = "\\r\\n";
            public const string R = "\\r";
            public const string N = "\\n";
        }
    }
}
