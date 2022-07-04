using System.Net.Http.Json;
using Drutol.FigureRepository.Shared.Braintree;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Drutol.FigureRepository.BlazorApp.Pages.Figures
{
    public partial class FigureDetail
    {
        [JSInvokable]
        public async Task SubmitPaypalNonce(string nonce)
        {
            using var content =
                JsonContent.Create(new BraintreeTransactionRequest(Figure.Guid, nonce, SelectedAccountAddress));
            var result = await HttpClient.Client.PostAsJsonAsync("api/braintree/token", content);

            Console.WriteLine(result.Content.ReadFromJsonAsync<BraintreeTransactionResponse>());
        }


        private async void OnBuyMenuOpened(MouseEventArgs obj)
        {
            var token = await HttpClient.Client.GetFromJsonAsync<BraintreeTokenResponse>("api/braintree/token");
            JS.InvokeVoidAsync("openPayPal", DotNetObjectReference.Create(this), token.Token,
                Figure.CheckoutDetails.Price);

        }
    }
}
