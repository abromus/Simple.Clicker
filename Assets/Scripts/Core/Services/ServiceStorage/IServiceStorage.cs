namespace Clicker.Core.Services
{
    public interface IServiceStorage
    {
        public TService GetService<TService>() where TService : class, IService;
    }
}
