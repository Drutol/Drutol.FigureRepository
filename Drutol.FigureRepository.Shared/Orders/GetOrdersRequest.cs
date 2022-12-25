using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Shared.Models.Orders;

namespace Drutol.FigureRepository.Shared.Orders;

public record GetOrdersRequest
{
    public List<OrderStatus> StatusFilter { get; init; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public int Take { get; set; }
    public int Skip { get; set; }
}

public record GetOrdersRequestResult(int TotalCount, List<Order> Orders);