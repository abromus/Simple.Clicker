namespace Clicker.Core.Services
{
    public interface ILocalizationSystem : IService
    {
        public string Get(string key);

        public string Get(string key, string value);

        public string Get(string key, string value1, string value2);

        public string Get(string key, string value1, string value2, string value3);
    }
}