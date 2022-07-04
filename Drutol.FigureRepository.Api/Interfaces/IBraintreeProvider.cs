using Drutol.FigureRepository.Shared.Braintree;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IBraintreeProvider
{
    ValueTask<string> GenerateToken();
    ValueTask<BraintreeTransactionResponse> CreateTransaction(BraintreeTransactionRequest nonce);
}