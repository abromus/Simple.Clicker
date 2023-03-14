using System;
using Clicker.Core.Services;
using Clicker.Core.Settings;
using Clicker.Core.World;
using UniRx;

namespace Clicker.Core
{
    public sealed class GameManagement : IGameManagement
    {
        private readonly IConfigStorage _configStorage;

        private readonly IServiceStorage _serviceStorage;
        private readonly IEcsManagement _ecsManagement;
        private readonly ISubject<Null> _viewUpdated;

        public IConfigStorage ConfigStorage => _configStorage;

        public IServiceStorage ServiceStorage => _serviceStorage;

        public IObservable<Null> ViewUpdated => _viewUpdated;

        public IWorld World => _serviceStorage.GetService<IEcsManagement>().World;

        public GameManagement(IConfigStorage configStorage)
        {
            _configStorage = configStorage;

            var serviceOptions = new ServiceOptions(
                this,
                _configStorage.GetUiConfig<ILocalizationConfig>(),
                _configStorage.GetUiConfig<IUiFactoryConfig>(),
                _configStorage.GetUiConfig<IUiServiceConfig>());

            _serviceStorage = new ServiceStorage(serviceOptions);

            _ecsManagement = _serviceStorage.GetService<IEcsManagement>();

            _viewUpdated = new Subject<Null>();
        }

        public void Tick(float deltaTime)
        {
            _ecsManagement.Tick(deltaTime);
        }

        public void ViewUpdate()
        {
            _viewUpdated.OnNext(null);
        }

        public void LateTick()
        {
            _ecsManagement.LateTick();
        }

        public void Destroy()
        {
            _ecsManagement.Destroy();
        }
    }
}
