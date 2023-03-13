namespace Clicker.Core.Services
{
    public interface ISaveManager : IService
    {
        public string Load();

        public void Save(string save);
    }
}
