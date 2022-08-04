using System.Diagnostics;
using System.Net.Http.Json;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using LoopringSharp;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Web3;
using Nethereum.Util;
using PoseidonSharp;

namespace Drutol.FigureRepository.BlazorApp.Pages.Figures;

public partial class FigureDetail 
{
    public bool IsFigureOwned { get; set; }
    public bool CanCheckOwnership { get; set; }

    private readonly HttpClient _loopringClient = new HttpClient
    {
        BaseAddress = new Uri("https://api.loopring.network/api/v3/")
    };

    [Inject] public IWalletProvider WalletProvider { get; set; }
        
    [Inject]
    public IApiHttpClient HttpClient { get; set; }

    private async Task UpdateCanCheckOwnership()
    {
        if (Figure == null || string.IsNullOrEmpty(WalletProvider.SelectedAccountAddress))
        {
            IsFigureOwned = false;
            return;
        }

        try
        {
            CanCheckOwnership = await GetLoopringAccount(WalletProvider.SelectedAccountAddress) switch
            {
                IAccountResponseModel.Success => true,
                _ => false,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CanCheckOwnership = false;
        }
    }

    private async Task UpdateOwnership()
    {
        if (Figure == null || string.IsNullOrEmpty(WalletProvider.SelectedAccountAddress) || !CanCheckOwnership)
        {
            IsFigureOwned = false;
            return;
        }

        try
        {
            //var sha = Sha3Keccack.Current.CalculateHash("balanceOf(address)");
            //var methodSignature = sha.Substring(0, 8); //first 4 bytes make the function signature
            //var methodArgument = SelectedAccountAddress[2..].PadLeft(64, '0'); //arguments have to be encoded as 32 bytes
            //var result = (JsonElement)await _metaMaskService.GenericRpc("eth_call", new
            //{
            //    to = Figure.NftDetails.TokenAddress,
            //    data = $"0x{methodSignature}{methodArgument}"
            //}, "latest");
            //var amount = (int)result.GetString().HexToBigInteger(false);

            //ethereum.request({
            //    method: 'personal_sign',
            //        params: ['Sign this message to access Loopring Exchange: 0x0BABA1Ad5bE3a5C0a66E7ac838a129Bf948f1eA4 with key nonce: 0', '0x59eb7c1e8e357ef2b4eb7532cab64c6538292ac6']
            //});

            using var authStartContent = JsonContent.Create(new StartAuthenticationRequest(
                StartAuthenticationRequest.AuthenticationType.Loopring,
                ((int?)WalletProvider.SelectedChain) ?? 0,
                WalletProvider.SelectedAccountAddress,
                Figure.Guid));
            var response = await
                (await HttpClient.Client.PostAsync("/api/auth/StartAuthentication", authStartContent)).Content.ReadFromJsonAsync<StartAuthenticationResult>();

            var signature = await WalletProvider.PersonalSign(response.DataToSign);

            var authFinishContent = JsonContent.Create(new FinishAuthenticationRequest(response.SessionGuid, signature));
            var auth = await
                (await HttpClient.Client.PostAsync("/api/auth/FinishAuthentication", authFinishContent)).Content.ReadFromJsonAsync<FinishAuthenticationResult>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            IsFigureOwned = false;
        }
    }

    private async void Download(MouseEventArgs obj)
    {
        using var authStartContent = JsonContent.Create(new StartAuthenticationRequest(
            StartAuthenticationRequest.AuthenticationType.Loopring,
            (int)WalletProvider.SelectedChain.Value,
            WalletProvider.SelectedAccountAddress,
            Figure.Guid));
        var response = await 
            (await HttpClient.Client.PostAsync("/api/auth/StartAuthentication", authStartContent)).Content.ReadFromJsonAsync<StartAuthenticationResult>();
            
        var signResult = await WalletProvider.SignTypedDataV4(response.DataToSign);

        var authFinishContent = JsonContent.Create(new FinishAuthenticationRequest(response.SessionGuid, signResult));
        var auth = await
            (await HttpClient.Client.PostAsync("/api/auth/FinishAuthentication", authFinishContent)).Content.ReadFromJsonAsync<FinishAuthenticationResult>();
    }


    private async ValueTask<IAccountResponseModel> GetLoopringAccount(string walletAddress)
    {
        try
        {
            var result = await _loopringClient
                .GetFromJsonAsync<IAccountResponseModel.Success>($"account?owner={walletAddress}")
                .ConfigureAwait(false);

            if (result == null)
                return new IAccountResponseModel.Fail();

            return result;
        }
        catch (Exception e)
        {
            return new IAccountResponseModel.Fail();
        }
    }
}