namespace Clicker.Core.Settings
{
    public interface IConfigStorage : IUiConfig
    {
        public TUiConfig GetUiConfig<TUiConfig>() where TUiConfig : class, IUiConfig;
    }
}
