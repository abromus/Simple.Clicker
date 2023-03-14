namespace Clicker.Core.Settings
{
    public interface IApplicationConfig : IUiConfig
    {
        public int TargetFrameRate { get; }
    }
}
