using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models
{
    public record FigureDownloadResource
    {
        public string Name { get; init; }
        public string Sha256 { get; init; }
        public FigureDownloadType FigureDownloadType { get; init; }
    }
}
