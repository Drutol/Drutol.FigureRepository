using System;
using System.Collections.Generic;
using Drutol.FigureRepository.Api.Models.Checkout;

namespace Drutol.FigureRepository.Shared.Models.Orders;

public class Order
{
    public Guid Guid { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid FigureId { get; set; }
    public decimal Price { get; set; }

    public string WalletAddress { get; set; }
    public int AccountId { get; set; }
    public bool IncludesWalletActivation { get; set; }
    public string PaidFee { get; set; }

    public string CheckoutId { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderEvent> Events { get; set; }
    public string PayPalObject { get; set; }
    public string TransactionHash { get; set; }
}