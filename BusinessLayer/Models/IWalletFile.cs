namespace BusinessLayer.Models
{
    public interface IWalletFile
    {
        string WalletFileName { get; set; }
        string WalletFilePath { get; set; }
    }
}