using System;
using System.Collections.Generic;
using Clicker.Core;
using Clicker.Core.Factories;
using Clicker.Core.Services;
using Clicker.Core.Settings;
using Clicker.Core.World;

namespace Clicker.Game.Screens
{
    public sealed class GameScreenOptions : BaseOptions
    {
        public IWorld World { get; }

        public IConfigStorage ConfigStorage { get; }

        public ILocalizationSystem LocalizationSystem { get; }

        public IObservable<Null> ViewUpdated { get; }

        public IReadOnlyList<IUiFactory> UiFactories { get; }

        public GameScreenOptions(
            IWorld world,
            IConfigStorage configStorage,
            ILocalizationSystem localizationSystem,
            IObservable<Null> viewUpdated,
            IReadOnlyList<IUiFactory> uiFactories)
        {
            World = world;
            ConfigStorage = configStorage;
            LocalizationSystem = localizationSystem;
            ViewUpdated = viewUpdated;
            UiFactories = uiFactories;
        }
    }
}
