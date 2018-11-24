namespace BusinessLayer.Configuration
{
    public interface IConfigFile
    {
        string CanSpendUnconfirmed { get; set; }
        string ConnectionType { get; set; }
        string DefaultWalletFileName { get; set; }
        string Network { get; set; }
    }
}