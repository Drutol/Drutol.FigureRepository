using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Api.Models.Checkout;

namespace Drutol.FigureRepository.Shared.Models.Orders
{
    public class OrderEvent
    {
        public OrderStatus StatusChange { get; init; }
        public DateTime DateTime { get; init; }
        public string Data { get; set; }
    }
}
