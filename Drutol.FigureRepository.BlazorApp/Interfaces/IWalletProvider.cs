using MetaMask.Blazor.Enums;

namespace Drutol.FigureRepository.BlazorApp.Interfaces;

public interface IWalletProvider
{
    event EventHandler StateHasChanged;
    bool EthereumAvailable { get; set; }
    bool HasMetaMask { get; set; }
    string SelectedAccountAddress { get; set; }
    Chain? SelectedChain { get; set; }
    Task Initialize();
    Task ConnectMetaMask();
    Task UpdateSelectedAddress();
    Task UpdateSelectedNetwork();
    Task<string> PersonalSign(string dataToSign);
    Task<string> SignTypedDataV4(string dataToSign);
}