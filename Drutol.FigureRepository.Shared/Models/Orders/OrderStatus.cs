namespace Drutol.FigureRepository.Api.Models.Checkout;

public enum OrderStatus
{
    Created = 0,

    Paid = 10,

    Delivered = 20,
    DeliveryPending = 21,

    Error = 1000,
    PayPalError = 1001,
        
}