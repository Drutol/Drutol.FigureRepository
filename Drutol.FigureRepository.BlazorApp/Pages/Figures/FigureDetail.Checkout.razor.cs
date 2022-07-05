using System.Net.Http.Json;
using System.Text.Json;
using Drutol.FigureRepository.Shared.Checkout;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Drutol.FigureRepository.BlazorApp.Pages.Figures
{
    public partial class FigureDetail
    {
        [JSInvokable]
        public async Task SubmitPaypalOrder(string orderId)
        {
            var result = await HttpClient.Client.PostAsJsonAsync("api/checkout/transaction", new CheckoutTransactionRequest(orderId));
            Console.WriteLine(result.Content.ReadFromJsonAsync<CheckoutTransactionResponse>());
        }


        private async void OnBuyMenuOpened(MouseEventArgs obj)
        {
            var reference = DotNetObjectReference.Create(this);
            var response = await HttpClient.Client.PostAsJsonAsync("api/checkout/order", new CheckoutOrderRequest(Figure.Guid, SelectedAccountAddress));
            var result = await response.Content.ReadFromJsonAsync<CheckoutOrderResponse>();
            await JS.InvokeVoidAsync("openPayPal", reference, result.OrderId);
        }
    }
}
