using Clicker.Core.Settings;

namespace Clicker.Game.Screens
{
    public sealed class BusinessViewOptions : BaseOptions
    {
        public GameScreenOptions GameOptions { get; }

        public int BusinessId { get; }

        public BusinessData BusinessData { get; }

        public BusinessViewOptions(GameScreenOptions options, int businessId, BusinessData businessData)
        {
            GameOptions = options;
            BusinessId = businessId;
            BusinessData = businessData;
        }
    }
}
