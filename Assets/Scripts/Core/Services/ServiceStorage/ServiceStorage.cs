using System;
using System.Collections.Generic;
using System.Linq;
using Clicker.Core.Factories;

namespace Clicker.Core.Services
{
    public sealed class ServiceStorage : IServiceStorage
    {
        private readonly Dictionary<Type, IService> _services;

        public ServiceStorage(IServiceOptions options)
        {
            var screenSystem = options.UiServiceConfig.UiServices
                .FirstOrDefault(service => service.UiServiceType == UiServiceType.ScreenSystem) as IScreenSystem;

            var factoryStorage = new FactoryStorage(options.UiFactoryConfig);
            var saveManager = new SaveManager(factoryStorage.SavePathProviderFactory);
            var saveSystem = new SaveSystem(saveManager);

            _services = new Dictionary<Type, IService>()
            {
                [typeof(IScreenSystem)] = screenSystem,
                [typeof(IFactoryStorage)] = factoryStorage,
                [typeof(ISaveManager)] = saveManager,
                [typeof(ISaveSystem)] = saveSystem,
                [typeof(ILocalizationSystem)] = new LocalizationSystem(options.LocalizationConfig),
                [typeof(IEcsManagement)] = new EcsManagement(saveSystem),
                [typeof(IStateMachine)] = new StateMachine(
                    options.GameManagement,
                    screenSystem,
                    new SceneLoader()),
            };
        }

        public TService GetService<TService>() where TService : class, IService
        {
            return _services[typeof(TService)] as TService;
        }
    }
}
