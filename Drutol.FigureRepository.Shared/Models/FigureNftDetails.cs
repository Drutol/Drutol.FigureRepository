namespace Drutol.FigureRepository.Shared.Models;

public record FigureNftDetails(string TokenAddress)
{
    public int TokenId { get; init; }
    public string NftData { get; set; }
    public string NftId { get; set; }
}