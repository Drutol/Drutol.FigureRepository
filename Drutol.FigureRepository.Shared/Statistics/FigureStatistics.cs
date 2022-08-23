using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Api.Models.Checkout;

namespace Drutol.FigureRepository.Shared.Statistics
{
    public record FigureStatistics(Guid FigureGuid, Dictionary<OrderStatus, int> OrderCounts);
}
