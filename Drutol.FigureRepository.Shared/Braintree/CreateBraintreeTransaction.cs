﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Braintree
{
    public record BraintreeTransactionRequest(Guid FigureGuid, string Nonce, string WalletAddress);

    public record BraintreeTransactionResponse();
}
