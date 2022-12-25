using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Orders;

public record DeliverPendingOrderRequest(Guid OrderGuid);

public record DeliverPendingOrderRequestResult(bool Success);

