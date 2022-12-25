using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Shared.Models.Orders;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface INftTransferProvider
{
    Task<NftTransferResult> TransferNft(Order order);
}