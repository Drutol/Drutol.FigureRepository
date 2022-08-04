namespace Drutol.FigureRepository.Api.Models.Blockchain;

public record NftTransferResult(bool Success)
{
    public string Fee { get; set; }
    public string Hash { get; set; }
}