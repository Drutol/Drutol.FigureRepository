using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Api.Models.Checkout;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface INftTransferProvider
{
    Task<NftTransferResult> TransferNft(OrderEntity order);
}