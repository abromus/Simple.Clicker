using System;
using System.Linq;

namespace Clicker.Core
{
    public class LocalizationSystem
    {
        private LocalizationConfig _config;

        public LocalizationSystem(LocalizationConfig localizationConfig)
        {
            _config = localizationConfig;
        }

        public string Get(string key)
        {
            var data = _config.LocalizationData.FirstOrDefault(item => item.Key == key);

            if (data == null)
            {
                return key;
            }
            else
            {
                var value = Trim(data.Value);

                return value;
            }
        }

        private string Trim(string data)
        {
            return data
                .Replace(LocalizationTrimKeys.RN, Environment.NewLine)
                .Replace(LocalizationTrimKeys.R, Environment.NewLine)
                .Replace(LocalizationTrimKeys.N, Environment.NewLine);
        }

        private class LocalizationTrimKeys
        {
            public const string RN = "\\r\\n";
            public const string R = "\\r";
            public const string N = "\\n";
        }
    }
}
