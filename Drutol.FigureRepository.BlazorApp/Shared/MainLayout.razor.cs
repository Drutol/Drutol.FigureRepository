using System.Net.Http.Json;
using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Infrastructure;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Drutol.FigureRepository.Shared.Models;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Drutol.FigureRepository.BlazorApp.Shared;

public partial class MainLayout
{
    [Inject] public IWalletProvider WalletProvider { get; set; }
    [Inject] public IFigureProvider FigureProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await WalletProvider.Initialize();

        _ = Task.Run(async () =>
        {
            await FigureProvider.Initialize();
        });
    }

    private async Task ConnectMetaMask()
    {
        await WalletProvider.ConnectMetaMask();
    }
}