namespace Drutol.FigureRepository.Shared.Models
{
    public record FigureNftDetails(string TokenAddress)
    {
        public int TokenId { get; init; }
        public string TokenNftData { get; set; }
    }
}
