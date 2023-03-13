using Clicker.Core.Services;
using Clicker.Core.Settings;
using Clicker.Core.World;

namespace Clicker.Game.Screens
{
    public sealed class ImprovementOptions : BaseOptions
    {
        public IWorld World { get; }

        public ILocalizationSystem LocalizationSystem { get; }

        public ImprovementData ImprovementData { get; }

        public int BusinessId { get; }

        public int ImprovementId { get; }

        public ImprovementOptions(
            IWorld world,
            ILocalizationSystem localizationSystem,
            ImprovementData improvementData,
            int id,
            int improvementId)
        {
            World = world;
            LocalizationSystem = localizationSystem;
            ImprovementData = improvementData;
            BusinessId = id;
            ImprovementId = improvementId;
        }
    }
}
