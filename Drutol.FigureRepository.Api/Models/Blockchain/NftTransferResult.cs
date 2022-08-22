namespace Drutol.FigureRepository.Api.Models.Blockchain;

public record NftTransferResult(bool Success)
{
    public string Fee { get; init; }
    public string Hash { get; init; }

    public List<string> ErrorMessages { get; init; }
}