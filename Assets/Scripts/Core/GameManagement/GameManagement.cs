using System;
using Clicker.Core.Services;
using Clicker.Core.Settings;
using Clicker.Core.World;
using UniRx;

namespace Clicker.Core
{
    public sealed class GameManagement : IGameManagement
    {
        private readonly ConfigData _configData;

        private readonly IServiceStorage _serviceStorage;
        private readonly IEcsManagement _ecsManagement;
        private readonly ISubject<Null> _viewUpdated;

        public ConfigData ConfigData => _configData;

        public IServiceStorage ServiceStorage => _serviceStorage;

        public IObservable<Null> ViewUpdated => _viewUpdated;

        public IWorld World => _serviceStorage.GetService<IEcsManagement>().World;

        public GameManagement(ConfigData configData)
        {
            _configData = configData;

            var serviceOptions = new ServiceOptions(
                this,
                _configData.LocalizationConfig,
                _configData.UiFactoryConfig,
                _configData.UiServiceConfig);

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
